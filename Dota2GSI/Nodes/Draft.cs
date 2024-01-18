using Dota2GSI.Nodes.DraftProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
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
        public readonly NodeMap<PlayerTeam, DraftDetails> Teams = new NodeMap<PlayerTeam, DraftDetails>();

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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Draft other &&
                ActiveTeam.Equals(other.ActiveTeam) &&
                Pick.Equals(other.Pick) &&
                ActiveTeamRemainingTime.Equals(other.ActiveTeamRemainingTime) &&
                RadiantBonusTime.Equals(other.RadiantBonusTime) &&
                DireBonusTime.Equals(other.DireBonusTime) &&
                Teams.Equals(other.Teams);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 370669188;
            hashCode = hashCode * -824566422 + ActiveTeam.GetHashCode();
            hashCode = hashCode * -824566422 + Pick.GetHashCode();
            hashCode = hashCode * -824566422 + ActiveTeamRemainingTime.GetHashCode();
            hashCode = hashCode * -824566422 + RadiantBonusTime.GetHashCode();
            hashCode = hashCode * -824566422 + DireBonusTime.GetHashCode();
            hashCode = hashCode * -824566422 + Teams.GetHashCode();
            return hashCode;
        }
    }
}
