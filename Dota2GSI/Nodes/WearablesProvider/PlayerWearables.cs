using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.WearablesProvider
{
    /// <summary>
    /// Class representing player wearables information.
    /// </summary>
    public class PlayerWearables : Node
    {
        /// <summary>
        /// The dictionary of player's wearable items.
        /// </summary>
        public readonly Dictionary<int, WearableItem> Wearables = new Dictionary<int, WearableItem>();

        private Regex _wearable_regex = new Regex(@"wearable(\d+)");
        private Regex _style_regex = new Regex(@"style(\d+)");

        internal PlayerWearables(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingIntegers(parsed_data, _wearable_regex, (Match match, int value) =>
            {
                var wearable_index = Convert.ToInt32(match.Groups[1].Value);

                if (!Wearables.ContainsKey(wearable_index))
                {
                    Wearables.Add(wearable_index, new WearableItem(value));
                }
                else
                {
                    var existing_wearable = Wearables[wearable_index];
                    Wearables[wearable_index] = new WearableItem(value, existing_wearable.Style);
                }
            });

            GetMatchingIntegers(parsed_data, _style_regex, (Match match, int value) =>
            {
                var wearable_index = Convert.ToInt32(match.Groups[1].Value);

                if (!Wearables.ContainsKey(wearable_index))
                {
                    Wearables.Add(wearable_index, new WearableItem(0, value));
                }
                else
                {
                    var existing_wearable = Wearables[wearable_index];
                    Wearables[wearable_index] = new WearableItem(existing_wearable.ID, value);
                }
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Wearables: {Wearables}" +
                $"]";
        }
    }
}
