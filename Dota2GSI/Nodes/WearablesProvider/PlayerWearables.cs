using Newtonsoft.Json.Linq;
using System;
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
        public readonly NodeMap<int, WearableItem> Wearables = new NodeMap<int, WearableItem>();

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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is PlayerWearables other &&
                Wearables.Equals(other.Wearables);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 954667287;
            hashCode = hashCode * -711254284 + Wearables.GetHashCode();
            return hashCode;
        }
    }
}
