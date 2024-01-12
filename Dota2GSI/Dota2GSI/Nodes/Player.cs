using Dota2GSI.Nodes.PlayerProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing player information.
    /// </summary>
    public class Player : Node
    {
        /// <summary>
        /// The local player's details.
        /// </summary>
        public readonly PlayerDetails LocalPlayer;

        /// <summary>
        /// The team player's details. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<PlayerTeam, Dictionary<int, PlayerDetails>> Teams = new Dictionary<PlayerTeam, Dictionary<int, PlayerDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Player(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player details
            LocalPlayer = new PlayerDetails(parsed_data);

            // Attempt to parse team player details
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new Dictionary<int, PlayerDetails>());
                }

                GetMatchingObjects(obj, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var player_details = new PlayerDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, player_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = player_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the player details for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>A dictionary of player id mapped to their player details.</returns>
        public Dictionary<int, PlayerDetails> GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new Dictionary<int, PlayerDetails>();
        }

        /// <summary>
        /// Gets the player details for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The player details.</returns>
        public PlayerDetails GetForPlayer(int player_id)
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

            return new PlayerDetails();
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
