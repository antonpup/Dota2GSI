using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.MinimapProvider
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
        public readonly PlayerTeam Team;

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

        internal MinimapElement(JObject parsed_data = null) : base(parsed_data)
        {
            Location = new Vector2D(GetInt("xpos"), GetInt("ypos"));
            RemainingTime = GetFloat("remainingtime");
            EventDuration = GetFloat("eventduration");
            Image = GetString("image");
            Team = GetEnum<PlayerTeam>("team");
            Name = GetString("name");
            Rotation = GetInt("yaw");
            UnitName = GetString("unitname");
            VisionRange = GetInt("visionrange");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Location: {Location}, " +
                $"RemainingTime: {RemainingTime}, " +
                $"EventDuration: {EventDuration}, " +
                $"Image: {Image}, " +
                $"Team: {Team}, " +
                $"Name: {Name}, " +
                $"Rotation: {Rotation}, " +
                $"UnitName: {UnitName}, " +
                $"VisionRange: {VisionRange}" +
                $"]";
        }
    }
}
