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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is MinimapElement other &&
                Location.Equals(other.Location) &&
                RemainingTime.Equals(other.RemainingTime) &&
                EventDuration.Equals(other.EventDuration) &&
                Image.Equals(other.Image) &&
                Team.Equals(other.Team) &&
                Name.Equals(other.Name) &&
                Rotation.Equals(other.Rotation) &&
                UnitName.Equals(other.UnitName) &&
                VisionRange.Equals(other.VisionRange);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 111327035;
            hashCode = hashCode * -71571866 + Location.GetHashCode();
            hashCode = hashCode * -71571866 + RemainingTime.GetHashCode();
            hashCode = hashCode * -71571866 + EventDuration.GetHashCode();
            hashCode = hashCode * -71571866 + Image.GetHashCode();
            hashCode = hashCode * -71571866 + Team.GetHashCode();
            hashCode = hashCode * -71571866 + Name.GetHashCode();
            hashCode = hashCode * -71571866 + Rotation.GetHashCode();
            hashCode = hashCode * -71571866 + UnitName.GetHashCode();
            hashCode = hashCode * -71571866 + VisionRange.GetHashCode();
            return hashCode;
        }
    }
}
