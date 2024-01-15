using Newtonsoft.Json.Linq;
using System;
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
        public readonly NodeMap<int, Building> TopTowers = new NodeMap<int, Building>();

        /// <summary>
        /// Middle towers.
        /// </summary>
        public readonly NodeMap<int, Building> MiddleTowers = new NodeMap<int, Building>();

        /// <summary>
        /// Bottom towers.
        /// </summary>
        public readonly NodeMap<int, Building> BottomTowers = new NodeMap<int, Building>();

        /// <summary>
        /// Top racks.
        /// </summary>
        public readonly NodeMap<RacksType, Building> TopRacks = new NodeMap<RacksType, Building>();

        /// <summary>
        /// Middle racks.
        /// </summary>
        public readonly NodeMap<RacksType, Building> MiddleRacks = new NodeMap<RacksType, Building>();

        /// <summary>
        /// Bottom racks.
        /// </summary>
        public readonly NodeMap<RacksType, Building> BottomRacks = new NodeMap<RacksType, Building>();

        /// <summary>
        /// Ancient.
        /// </summary>
        public readonly Building Ancient = new Building();

        /// <summary>
        /// Other buildings.
        /// </summary>
        public readonly NodeMap<string, Building> OtherBuildings = new NodeMap<string, Building>();

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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is BuildingLayout other &&
                TopTowers.Equals(other.TopTowers) &&
                MiddleTowers.Equals(other.MiddleTowers) &&
                BottomTowers.Equals(other.BottomTowers) &&
                TopRacks.Equals(other.TopRacks) &&
                MiddleRacks.Equals(other.MiddleRacks) &&
                BottomRacks.Equals(other.BottomRacks) &&
                Ancient.Equals(other.Ancient) &&
                OtherBuildings.Equals(other.OtherBuildings);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 946659162;
            hashCode = hashCode * -439837856 + TopTowers.GetHashCode();
            hashCode = hashCode * -439837856 + MiddleTowers.GetHashCode();
            hashCode = hashCode * -439837856 + BottomTowers.GetHashCode();
            hashCode = hashCode * -439837856 + TopRacks.GetHashCode();
            hashCode = hashCode * -439837856 + MiddleRacks.GetHashCode();
            hashCode = hashCode * -439837856 + BottomRacks.GetHashCode();
            hashCode = hashCode * -439837856 + Ancient.GetHashCode();
            hashCode = hashCode * -439837856 + OtherBuildings.GetHashCode();
            return hashCode;
        }
    }
}
