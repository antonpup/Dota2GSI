using Dota2GSI.Nodes.HeroProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing hero information.
    /// </summary>
    public class Hero : Node
    {
        /// <summary>
        /// The local player's hero details.
        /// </summary>
        public readonly HeroDetails LocalPlayer;

        /// <summary>
        /// The team players hero details. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<PlayerTeam, Dictionary<int, HeroDetails>> Teams = new Dictionary<PlayerTeam, Dictionary<int, HeroDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Hero(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local hero details
            LocalPlayer = new HeroDetails(parsed_data);

            // Attempt to parse team hero details
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new Dictionary<int, HeroDetails>());
                }

                GetMatchingObjects(obj, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var hero_details = new HeroDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, hero_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = hero_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the hero details for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>A dictionary of player id mapped to their hero details.</returns>
        public Dictionary<int, HeroDetails> GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new Dictionary<int, HeroDetails>();
        }

        /// <summary>
        /// Gets the hero details for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The hero details.</returns>
        public HeroDetails GetForPlayer(int player_id)
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

            return new HeroDetails();
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
