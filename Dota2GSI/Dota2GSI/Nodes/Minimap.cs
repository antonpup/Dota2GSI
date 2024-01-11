using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing a minimap element.
    /// </summary>
    public class MinimapElement : Node
    {
        /// <summary>
        /// Location of the element on the minimap.
        /// </summary>
        public readonly Vector2D Location;

        /// <summary>
        /// Remaining time of the element.
        /// </summary>
        public readonly float RemainingTime;

        /// <summary>
        /// Event duration of the element.
        /// </summary>
        public readonly float EventDuration;

        /// <summary>
        /// Image of the element.
        /// </summary>
        public readonly string Image;

        /// <summary>
        /// Team of the element.
        /// </summary>
        public readonly int TeamID;

        /// <summary>
        /// Name of the element.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The yaw rotation of the element.
        /// </summary>
        public readonly int Rotation;

        /// <summary>
        /// Unit name of the element.
        /// </summary>
        public readonly string UnitName;

        /// <summary>
        /// Vision range of the element.
        /// </summary>
        public readonly int VisionRange;

        public MinimapElement(JObject parsed_data = null) : base(parsed_data)
        {
            Location = new Vector2D(GetInt("xpos"), GetInt("ypos"));
            RemainingTime = GetFloat("remainingtime");
            EventDuration = GetFloat("eventduration");
            Image = GetString("image");
            TeamID = GetInt("team");
            Name = GetString("name");
            Rotation = GetInt("yaw");
            UnitName = GetString("unitname");
            VisionRange = GetInt("visionrange");
        }
    }

    /// <summary>
    /// Class representing the minimap.
    /// </summary>
    public class Minimap : Node
    {
        /// <summary>
        /// The minimap elements
        /// </summary>
        public readonly Dictionary<int, MinimapElement> Elements = new Dictionary<int, MinimapElement>();

        private Regex _object_regex = new Regex(@"o(\d+)");
        internal Minimap(JObject parsed_data = null) : base(parsed_data)
        {
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_object_regex.IsMatch(property_name) && property.Value.Type == JTokenType.Object)
                    {
                        var match = _object_regex.Match(property_name);
                        var object_index = Convert.ToInt32(match.Groups[1].Value);

                        Elements.Add(object_index, new MinimapElement(property.Value as JObject));
                    }
                }
            }
        }
    }
}
