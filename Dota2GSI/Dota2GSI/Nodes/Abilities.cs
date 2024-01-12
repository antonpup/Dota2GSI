using Dota2GSI.Nodes.AbilitiesProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing hero abilities.
    /// </summary>
    public class Abilities : Node
    {
        /// <summary>
        /// The local player's ability details.
        /// </summary>
        public readonly AbilityDetails LocalPlayer;

        /// <summary>
        /// The team players ability details. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<PlayerTeam, Dictionary<int, AbilityDetails>> Teams = new Dictionary<PlayerTeam, Dictionary<int, AbilityDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Abilities(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local hero abilities
            LocalPlayer = new AbilityDetails(parsed_data);

            // Attempt to parse team hero wearables
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new Dictionary<int, AbilityDetails>());
                }

                GetMatchingObjects(obj, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var ability_details = new AbilityDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, ability_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = ability_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the abilities for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>A dictionary of player id mapped to their ability details.</returns>
        public Dictionary<int, AbilityDetails> GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new Dictionary<int, AbilityDetails>();
        }

        /// <summary>
        /// Gets the abilities for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The ability details.</returns>
        public AbilityDetails GetForPlayer(int player_id)
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

            return new AbilityDetails();
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
    }
}
