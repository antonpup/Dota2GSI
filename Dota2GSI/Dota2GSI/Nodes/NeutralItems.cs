using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Enum for different states of neutral items.
    /// </summary>
    public enum NeutralItemState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// In stash.
        /// </summary>
        Stash,

        /// <summary>
        /// Consumed.
        /// </summary>
        Consumed,

        /// <summary>
        /// Equipped.
        /// </summary>
        Equipped,

        /// <summary>
        /// In backpack.
        /// </summary>
        Backpack,

        /// <summary>
        /// In player's stash.
        /// </summary>
        Player_Stash,

        /// <summary>
        /// On courier.
        /// </summary>
        Courier
    }

    /// <summary>
    /// Class representing neutral item tier information.
    /// </summary>
    public class NeutralTierInfo : Node
    {
        /// <summary>
        /// The neutral item's tier.
        /// </summary>
        public readonly int Tier;

        /// <summary>
        /// The neutral item's max count for the tier.
        /// </summary>
        public readonly int MaxCount;

        /// <summary>
        /// The neutral item's availability to drop for the tier.
        /// </summary>
        public readonly int DropAfterTime;

        internal NeutralTierInfo(JObject parsed_data = null) : base(parsed_data)
        {
            Tier = GetInt("tier");
            MaxCount = GetInt("max_count");
            DropAfterTime = GetInt("drop_after_time");
        }
    }

    /// <summary>
    /// Class representing neutral item information.
    /// </summary>
    public class NeutralItem : Node
    {
        /// <summary>
        /// The neutral item's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The neutral item's name.
        /// </summary>
        public readonly int Tier;

        /// <summary>
        /// The neutral item's charges.
        /// </summary>
        public readonly int Charges;

        /// <summary>
        /// The neutral item's state.
        /// </summary>
        public readonly NeutralItemState State;

        /// <summary>
        /// The neutral item's possessing player ID.
        /// </summary>
        public readonly int PlayerID;

        internal NeutralItem(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            Tier = GetInt("tier");
            Charges = GetInt("charges");
            State = GetEnum<NeutralItemState>("state");
            PlayerID = GetInt("player_id");
        }
    }

    /// <summary>
    /// Class representing team neutral items.
    /// </summary>
    public class TeamNeutralItems : Node
    {
        /// <summary>
        /// The number of neutral items found.
        /// </summary>
        public readonly int ItemsFound;

        /// <summary>
        /// The team neutral items.
        /// </summary>
        public readonly Dictionary<int, Dictionary<int, NeutralItem>> TeamItems = new Dictionary<int, Dictionary<int, NeutralItem>>();

        private Regex _tier_id_regex = new Regex(@"tier(\d+)");
        private Regex _item_id_regex = new Regex(@"item(\d+)");

        internal TeamNeutralItems(JObject parsed_data = null) : base(parsed_data)
        {
            ItemsFound = GetInt("items_found");

            GetMatchingObjects(parsed_data, _tier_id_regex, (Match match, JObject obj) =>
            {
                var tier_index = Convert.ToInt32(match.Groups[1].Value);

                if (!TeamItems.ContainsKey(tier_index))
                {
                    TeamItems.Add(tier_index, new Dictionary<int, NeutralItem>());
                }

                GetMatchingObjects(obj, _item_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var item_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var neutral_item = new NeutralItem(sub_obj);

                    if (!TeamItems[tier_index].ContainsKey(item_index))
                    {
                        TeamItems[tier_index].Add(item_index, neutral_item);
                    }
                    else
                    {
                        TeamItems[tier_index][item_index] = neutral_item;
                    }
                });
            });
        }
    }

    /// <summary>
    /// Class representing neutral items.
    /// </summary>
    public class NeutralItems : Node
    {
        /// <summary>
        /// Information about various neutral item tiers.
        /// </summary>
        public readonly Dictionary<int, NeutralTierInfo> TierInfos = new Dictionary<int, NeutralTierInfo>();

        /// <summary>
        /// Information about team's neutral items.
        /// </summary>
        public readonly Dictionary<PlayerTeam, TeamNeutralItems> TeamItems = new Dictionary<PlayerTeam, TeamNeutralItems>();

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
        /// <param name="team_id">The team.</param>
        /// <returns>The neutral items details.</returns>
        public TeamNeutralItems GetTeam(PlayerTeam team)
        {
            if (TeamItems.ContainsKey(team))
            {
                return TeamItems[team];
            }

            return new TeamNeutralItems();
        }
    }
}
