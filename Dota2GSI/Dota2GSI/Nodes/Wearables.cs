using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing wearable items.
    /// </summary>
    public class Wearables : Node
    {
        /// <summary>
        /// The local player's wearables.
        /// </summary>
        public readonly PlayerWearables PlayerWearables;

        /// <summary>
        /// The team players wearables. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<int, Dictionary<int, PlayerWearables>> TeamWearables = new Dictionary<int, Dictionary<int, PlayerWearables>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Wearables(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player wearables
            PlayerWearables = new PlayerWearables(parsed_data);

            // Attempt to parse team player wearables
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_team_id_regex.IsMatch(property_name) && property.Value.Type == JTokenType.Object)
                    {
                        var match = _team_id_regex.Match(property_name);
                        var team_index = Convert.ToInt32(match.Groups[1].Value);

                        if (!TeamWearables.ContainsKey(team_index))
                        {
                            TeamWearables.Add(team_index, new Dictionary<int, PlayerWearables>());
                        }

                        foreach (var sub_property in (property.Value as JObject).Properties())
                        {
                            string sub_property_name = property.Name;

                            if (_player_id_regex.IsMatch(sub_property_name) && sub_property.Value.Type == JTokenType.Object)
                            {
                                var sub_match = _player_id_regex.Match(sub_property_name);
                                var player_index = Convert.ToInt32(sub_match.Groups[1].Value);

                                var player_wearables = new PlayerWearables(sub_property.Value as JObject);

                                if (!TeamWearables[team_index].ContainsKey(player_index))
                                {
                                    TeamWearables[team_index].Add(player_index, player_wearables);
                                }
                                else
                                {
                                    TeamWearables[team_index][player_index] = player_wearables;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
