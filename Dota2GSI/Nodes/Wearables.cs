using Dota2GSI.Nodes.WearablesProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing wearable items.
    /// </summary>
    public class Wearables : Node
    {
        /// <summary>
        /// The local player's wearables.
        /// </summary>
        public readonly PlayerWearables LocalPlayer;

        /// <summary>
        /// The team players wearables. (SPECTATOR ONLY)
        /// </summary>
        public readonly NodeMap<PlayerTeam, NodeMap<int, PlayerWearables>> Teams = new NodeMap<PlayerTeam, NodeMap<int, PlayerWearables>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Wearables(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player wearables
            LocalPlayer = new PlayerWearables(parsed_data);

            // Attempt to parse team player wearables
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new NodeMap<int, PlayerWearables>());
                }

                GetMatchingObjects(parsed_data, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var player_wearables = new PlayerWearables(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, player_wearables);
                    }
                    else
                    {
                        Teams[team_id][player_index] = player_wearables;
                    }

                });
            });
        }

        /// <summary>
        /// Gets the wearables for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>A dictionary of player id mapped to their wearables.</returns>
        public NodeMap<int, PlayerWearables> GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new NodeMap<int, PlayerWearables>();
        }

        /// <summary>
        /// Gets the wearables for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The player wearables.</returns>
        public PlayerWearables GetForPlayer(int player_id)
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

            return new PlayerWearables();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"LocalPlayer: {LocalPlayer}, " +
                $"Teams: {Teams}" +
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

            return obj is Wearables other &&
                LocalPlayer.Equals(other.LocalPlayer) &&
                Teams.Equals(other.Teams);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 894234507;
            hashCode = hashCode * -274381118 + LocalPlayer.GetHashCode();
            hashCode = hashCode * -274381118 + Teams.GetHashCode();
            return hashCode;
        }
    }
}
