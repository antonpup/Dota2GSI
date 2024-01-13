using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.NeutralItemsProvider
{
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"ItemsFound: {ItemsFound}, " +
                $"TeamItems: {TeamItems}" +
                $"]";
        }
    }
}
