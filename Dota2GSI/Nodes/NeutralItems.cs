using Dota2GSI.Nodes.NeutralItemsProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing neutral items.
    /// </summary>
    public class NeutralItems : Node
    {
        /// <summary>
        /// Information about various neutral item tiers.
        /// </summary>
        public readonly NodeMap<int, NeutralTierInfo> TierInfos = new NodeMap<int, NeutralTierInfo>();

        /// <summary>
        /// Information about team's neutral items.
        /// </summary>
        public readonly NodeMap<PlayerTeam, TeamNeutralItems> TeamItems = new NodeMap<PlayerTeam, TeamNeutralItems>();

        private Regex _tier_id_regex = new Regex(@"tier(\d+)");
        private Regex _team_id_regex = new Regex(@"team(\d+)");

        internal NeutralItems(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse team player wearables
            GetMatchingObjects(parsed_data, _tier_id_regex, (Match match, JObject obj) =>
            {
                var tier_index = Convert.ToInt32(match.Groups[1].Value);
                var tier_info = new NeutralTierInfo(obj);

                if (!TierInfos.ContainsKey(tier_index))
                {
                    TierInfos.Add(tier_index, tier_info);
                }
                else
                {
                    TierInfos[tier_index] = tier_info;
                }
            });

            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);
                var team_items = new TeamNeutralItems(obj);

                if (!TeamItems.ContainsKey(team_id))
                {
                    TeamItems.Add(team_id, team_items);
                }
                else
                {
                    TeamItems[team_id] = team_items;
                }
            });
        }

        /// <summary>
        /// Gets the neutral items for a specific team.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>The neutral items details.</returns>
        public TeamNeutralItems GetForTeam(PlayerTeam team)
        {
            if (TeamItems.ContainsKey(team))
            {
                return TeamItems[team];
            }

            return new TeamNeutralItems();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"TierInfos: {TierInfos}, " +
                $"TeamItems: {TeamItems}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is NeutralItems other &&
                TierInfos.Equals(other.TierInfos) &&
                TeamItems.Equals(other.TeamItems);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 904745338;
            hashCode = hashCode * -700564887 + TierInfos.GetHashCode();
            hashCode = hashCode * -700564887 + TeamItems.GetHashCode();
            return hashCode;
        }
    }
}
