using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.RoshanProvider
{
    /// <summary>
    /// Class representing item drops.
    /// </summary>
    public class ItemsDrop : Node
    {
        /// <summary>
        /// Items that can drop.
        /// </summary>
        public readonly Dictionary<int, string> Items = new Dictionary<int, string>();

        private Regex _item_regex = new Regex(@"item(\d+)");
        internal ItemsDrop(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingStrings(parsed_data, _item_regex, (Match match, string value) =>
            {
                var item_index = Convert.ToInt32(match.Groups[1].Value);

                Items.Add(item_index, value);
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Items: {Items}" +
                $"]";
        }
    }
}
