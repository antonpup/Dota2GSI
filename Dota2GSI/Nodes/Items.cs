using Dota2GSI.Nodes.ItemsProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item information.
    /// </summary>
    public class Items : Node
    {
        /// <summary>
        /// The local player's items.
        /// </summary>
        public readonly ItemDetails LocalPlayer;

        /// <summary>
        /// The team players items. (SPECTATOR ONLY)
        /// </summary>
        public readonly NodeMap<PlayerTeam, NodeMap<int, ItemDetails>> Teams = new NodeMap<PlayerTeam, NodeMap<int, ItemDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Items(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player items
            LocalPlayer = new ItemDetails(parsed_data);

            // Attempt to parse team players items
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new NodeMap<int, ItemDetails>());
                }

                GetMatchingObjects(parsed_data, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var item_details = new ItemDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, item_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = item_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the item details for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>A dictionary of player id mapped to their item details.</returns>
        public NodeMap<int, ItemDetails> GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new NodeMap<int, ItemDetails>();
        }

        /// <summary>
        /// Gets the item details for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The player item details.</returns>
        public ItemDetails GetForPlayer(int player_id)
        {
            foreach (var team in Teams)
            {
                foreach (var player in team.Value)
                {
                    if (player.Key == player_id)
                    {
                        return player.Value;
                    }
                }
            }

            return new ItemDetails();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"LocalPlayer: {LocalPlayer}, " +
                $"Teams: {Teams}, " +
                $"]";
        }

        /// <inheritdoc/>
        public override bool IsValid()
        {
            return LocalPlayer.IsValid() || base.IsValid();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Items other &&
                LocalPlayer.Equals(other.LocalPlayer) &&
                Teams.Equals(other.Teams);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 787251810;
            hashCode = hashCode * -635208756 + LocalPlayer.GetHashCode();
            hashCode = hashCode * -635208756 + Teams.GetHashCode();
            return hashCode;
        }
    }
}
