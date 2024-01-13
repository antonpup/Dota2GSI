using Dota2GSI.Nodes.CouriersProvider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing couriers.
    /// </summary>
    public class Couriers : Node
    {
        /// <summary>
        /// A dictionary mapping of courier ID to courier.
        /// </summary>
        public readonly Dictionary<int, Courier> CouriersMap = new Dictionary<int, Courier>();

        private Regex _courier_regex = new Regex(@"courier(\d+)");

        internal Couriers(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingObjects(parsed_data, _courier_regex, (Match match, JObject obj) =>
            {
                var item_index = Convert.ToInt32(match.Groups[1].Value);
                var courier = new Courier(obj);

                CouriersMap.Add(item_index, courier);
            });
        }

        /// <summary>
        /// Gets the courier for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The courier.</returns>
        public Courier GetForPlayer(int player_id)
        {
            foreach (var courier in CouriersMap)
            {
                if (courier.Value.OwnerID == player_id)
                {
                    return courier.Value;
                }
            }

            return new Courier();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"CouriersMap: {CouriersMap}" +
                $"]";
        }
    }
}
