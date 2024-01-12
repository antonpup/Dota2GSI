using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing draft details information.
    /// </summary>
    public class DraftDetails : Node
    {
        /// <summary>
        /// Is home team.
        /// </summary>
        public readonly bool IsHomeTeam;

        /// <summary>
        /// Pick IDs.
        /// </summary>
        public readonly Dictionary<int, int> PickIDs = new Dictionary<int, int>();

        /// <summary>
        /// Pick Hero IDs.
        /// </summary>
        public readonly Dictionary<int, string> PickHeroIDs = new Dictionary<int, string>();

        private Regex _pick_id_regex = new Regex(@"pick(\d+)_id");
        private Regex _pick_class_regex = new Regex(@"pick(\d+)_class");

        internal DraftDetails(JObject parsed_data = null) : base(parsed_data)
        {
            IsHomeTeam = GetBool("home_team");

            GetMatchingIntegers(parsed_data, _pick_id_regex, (Match match, int value) =>
            {
                var pick_index = Convert.ToInt32(match.Groups[1].Value);

                PickIDs.Add(pick_index, value);
            });

            GetMatchingStrings(parsed_data, _pick_class_regex, (Match match, string value) =>
            {
                var pick_index = Convert.ToInt32(match.Groups[1].Value);

                PickHeroIDs.Add(pick_index, value);
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"IsHomeTeam: {IsHomeTeam}, " +
                $"PickIDs: {PickIDs}, " +
                $"PickHeroIDs: {PickHeroIDs}" +
                $"]";
        }
    }

    /// <summary>
    /// A class representing draft information.
    /// </summary>
    public class Draft : Node
    {
        /// <summary>
        /// The active team.
        /// </summary>
        public readonly int ActiveTeam;

        /// <summary>
        /// Is hero picking state. Ban state if false.
        /// </summary>
        public readonly bool Pick;

        /// <summary>
        /// The active team remaining time in seconds.
        /// </summary>
        public readonly int ActiveTeamRemainingTime;

        /// <summary>
        /// The radiant team bonus time in seconds.
        /// </summary>
        public readonly int RadiantBonusTime;

        /// <summary>
        /// The dire team bonus time in seconds.
        /// </summary>
        public readonly int DireBonusTime;

        /// <summary>
        /// The team draft information.
        /// </summary>
        public readonly Dictionary<PlayerTeam, DraftDetails> Teams = new Dictionary<PlayerTeam, DraftDetails>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");

        internal Draft(JObject parsed_data = null) : base(parsed_data)
        {
            ActiveTeam = GetInt("activeteam");
            Pick = GetBool("pick");
            ActiveTeamRemainingTime = GetInt("activeteam_time_remaining");
            RadiantBonusTime = GetInt("radiant_bonus_time");
            DireBonusTime = GetInt("dire_bonus_time");

            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                Teams.Add(team_id, new DraftDetails(obj));
            });
        }

        /// <summary>
        /// Gets the draft for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>The draft details.</returns>
        public DraftDetails GetForTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new DraftDetails();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"ActiveTeam: {ActiveTeam}, " +
                $"Pick: {Pick}, " +
                $"ActiveTeamRemainingTime: {ActiveTeamRemainingTime}, " +
                $"RadiantBonusTime: {RadiantBonusTime}, " +
                $"DireBonusTime: {DireBonusTime}, " +
                $"Teams: {Teams}" +
                $"]";
        }
    }
}
