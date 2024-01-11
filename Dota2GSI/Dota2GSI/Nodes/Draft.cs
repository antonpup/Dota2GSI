using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing team draft information.
    /// </summary>
    public class DraftTeam : Node
    {
        /// <summary>
        /// Is home team.
        /// </summary>
        public readonly bool IsHomeTeam;

        /// <summary>
        /// Pick IDs.
        /// </summary>
        public readonly Dictionary<int, int> PickIDs;

        /// <summary>
        /// Pick Hero IDs.
        /// </summary>
        public readonly Dictionary<int, string> PickHeroIDs;

        private Regex _pick_id_regex = new Regex(@"pick(\d+)_id");
        private Regex _pick_class_regex = new Regex(@"pick(\d+)_class");

        internal DraftTeam(JObject parsed_data = null) : base(parsed_data)
        {
            IsHomeTeam = GetBool("home_team");

            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_pick_id_regex.IsMatch(property_name))
                    {
                        var match = _pick_id_regex.Match(property_name);

                        var pick_index = Convert.ToInt32(match.Groups[1].Value);
                        var pick = Convert.ToInt32(property.Value);

                        PickIDs.Add(pick_index, pick);
                    }
                    else if (_pick_class_regex.IsMatch(property_name))
                    {
                        var match = _pick_class_regex.Match(property_name);

                        var pick_index = Convert.ToInt32(match.Groups[1].Value);
                        var pick = property.Value.ToString();

                        PickHeroIDs.Add(pick_index, pick);
                    }
                }
            }
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
        /// The pick?
        /// </summary>
        public readonly bool Pick;

        /// <summary>
        /// The active team remaining time.
        /// </summary>
        public readonly int ActiveTeamRemainingTime;

        /// <summary>
        /// The radiant team bonus time.
        /// </summary>
        public readonly int RadiantBonusTime;

        /// <summary>
        /// The dire team bonus time.
        /// </summary>
        public readonly int DireBonusTime;

        /// <summary>
        /// The team draft information.
        /// </summary>
        public readonly Dictionary<int, DraftTeam> Teams = new Dictionary<int, DraftTeam>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");

        internal Draft(JObject parsed_data = null) : base(parsed_data)
        {
            ActiveTeam = GetInt("activeteam");
            Pick = GetBool("pick");
            ActiveTeamRemainingTime = GetInt("activeteam_time_remaining");
            RadiantBonusTime = GetInt("radiant_bonus_time");
            DireBonusTime = GetInt("dire_bonus_time");

            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_team_id_regex.IsMatch(property_name) && property.Value.Type == JTokenType.Object)
                    {
                        var match = _team_id_regex.Match(property_name);
                        var team_index = Convert.ToInt32(match.Groups[1].Value);

                        Teams.Add(team_index, new DraftTeam(property.Value as JObject));
                    }
                }
            }
        }
    }
}
