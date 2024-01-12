using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.BuildingsProvider
{
    /// <summary>
    /// Enum for racks types.
    /// </summary>
    public enum RacksType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Melee racks.
        /// </summary>
        Melee,

        /// <summary>
        /// Ranged racks.
        /// </summary>
        Range
    }

    /// <summary>
    /// Class representing buildings.
    /// </summary>
    public class BuildingLayout
    {
        /// <summary>
        /// Top towers.
        /// </summary>
        public readonly Dictionary<int, Building> TopTowers = new Dictionary<int, Building>();

        /// <summary>
        /// Middle towers.
        /// </summary>
        public readonly Dictionary<int, Building> MiddleTowers = new Dictionary<int, Building>();

        /// <summary>
        /// Bottom towers.
        /// </summary>
        public readonly Dictionary<int, Building> BottomTowers = new Dictionary<int, Building>();

        /// <summary>
        /// Top racks.
        /// </summary>
        public readonly Dictionary<RacksType, Building> TopRacks = new Dictionary<RacksType, Building>();

        /// <summary>
        /// Middle racks.
        /// </summary>
        public readonly Dictionary<RacksType, Building> MiddleRacks = new Dictionary<RacksType, Building>();

        /// <summary>
        /// Bottom racks.
        /// </summary>
        public readonly Dictionary<RacksType, Building> BottomRacks = new Dictionary<RacksType, Building>();

        /// <summary>
        /// Ancient.
        /// </summary>
        public readonly Building Ancient = new Building();

        /// <summary>
        /// Other buildings.
        /// </summary>
        public readonly Dictionary<string, Building> OtherBuildings = new Dictionary<string, Building>();

        private Regex _tower_regex = new Regex(@"tower(\d+)_(top|mid|bot)");
        private Regex _racks_regex = new Regex(@"rax_(melee|range)_(top|mid|bot)");
        private Regex _ancient_regex = new Regex(@"_fort");

        internal BuildingLayout(JObject parsed_data = null)
        {
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;
                    var building = new Building(property.Value as JObject);

                    if (_tower_regex.IsMatch(property_name))
                    {
                        var match = _tower_regex.Match(property_name);

                        var tower_index = Convert.ToInt32(match.Groups[1].Value);
                        var tower_lane = match.Groups[2].Value;

                        if (tower_lane.Equals("top"))
                        {
                            TopTowers.Add(tower_index, building);
                        }
                        else if (tower_lane.Equals("mid"))
                        {
                            MiddleTowers.Add(tower_index, building);
                        }
                        else if (tower_lane.Equals("bot"))
                        {
                            BottomTowers.Add(tower_index, building);
                        }
                    }
                    else if (_racks_regex.IsMatch(property_name))
                    {
                        var match = _racks_regex.Match(property_name);

                        RacksType rax_type = RacksType.Undefined;
                        var rax_lane = match.Groups[2].Value;

                        if (match.Groups[1].Value.Equals("melee"))
                        {
                            rax_type = RacksType.Melee;
                        }
                        else if (match.Groups[1].Value.Equals("range"))
                        {
                            rax_type = RacksType.Range;
                        }

                        if (rax_type != RacksType.Undefined)
                        {
                            if (rax_lane.Equals("top"))
                            {
                                TopRacks.Add(rax_type, building);
                            }
                            else if (rax_lane.Equals("mid"))
                            {
                                MiddleRacks.Add(rax_type, building);
                            }
                            else if (rax_lane.Equals("bot"))
                            {
                                BottomRacks.Add(rax_type, building);
                            }
                        }
                    }
                    else if (_ancient_regex.IsMatch(property_name))
                    {
                        Ancient = building;
                    }
                    else
                    {
                        OtherBuildings.Add(property_name, building);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"TopTowers: {TopTowers}, " +
                $"MiddleTowers: {MiddleTowers}, " +
                $"BottomTowers: {BottomTowers}, " +
                $"TopRacks: {TopRacks}, " +
                $"MiddleRacks: {MiddleRacks}, " +
                $"BottomRacks: {BottomRacks}, " +
                $"Ancient: {Ancient}, " +
                $"OtherBuildings: {OtherBuildings}" +
                $"]";
        }
    }
}
