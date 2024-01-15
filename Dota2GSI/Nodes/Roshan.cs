using Dota2GSI.Nodes.Helpers;
using Dota2GSI.Nodes.RoshanProvider;
using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing Roshan information.
    /// </summary>
    public class Roshan : Node
    {
        /// <summary>
        /// Roshan's health.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// Roshan's maximum health.
        /// </summary>
        public readonly int MaxHealth;

        /// <summary>
        /// Is Roshan alive.
        /// </summary>
        public readonly bool IsAlive;

        /// <summary>
        /// Roshan's spawn phase.
        /// </summary>
        public readonly int SpawnPhase;

        /// <summary>
        /// Roshan's remaining phase time.
        /// </summary>
        public readonly float PhaseTimeRemaining;

        /// <summary>
        /// Roshan's location.
        /// </summary>
        public readonly Vector2D Location;

        /// <summary>
        /// Roshan's yaw rotation.
        /// </summary>
        public readonly int Rotation;

        /// <summary>
        /// Roshan's item drops when killed.
        /// </summary>
        public readonly ItemsDrop Drops;

        internal Roshan(JObject parsed_data = null) : base(parsed_data)
        {
            Location = new Vector2D(GetInt("xpos"), GetInt("ypos"));
            Health = GetInt("health");
            MaxHealth = GetInt("max_health");
            IsAlive = GetBool("alive");
            SpawnPhase = GetInt("spawn_phase");
            PhaseTimeRemaining = GetFloat("phase_time_remaining");
            Rotation = GetInt("yaw");
            Drops = new ItemsDrop(GetJObject("items_drop"));
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Health: {Health}, " +
                $"MaxHealth: {MaxHealth}, " +
                $"IsAlive: {IsAlive}, " +
                $"SpawnPhase: {SpawnPhase}, " +
                $"PhaseTimeRemaining: {PhaseTimeRemaining}, " +
                $"Location: {Location}, " +
                $"Rotation: {Rotation}, " +
                $"Drops: {Drops}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Roshan other &&
                Health == other.Health &&
                MaxHealth == other.MaxHealth &&
                IsAlive == other.IsAlive &&
                SpawnPhase == other.SpawnPhase &&
                PhaseTimeRemaining == other.PhaseTimeRemaining &&
                Location.Equals(other.Location) &&
                Rotation == other.Rotation &&
                Drops.Equals(other.Drops);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 289605977;
            hashCode = hashCode * -839892812 + Health.GetHashCode();
            hashCode = hashCode * -839892812 + MaxHealth.GetHashCode();
            hashCode = hashCode * -839892812 + IsAlive.GetHashCode();
            hashCode = hashCode * -839892812 + SpawnPhase.GetHashCode();
            hashCode = hashCode * -839892812 + PhaseTimeRemaining.GetHashCode();
            hashCode = hashCode * -839892812 + Location.GetHashCode();
            hashCode = hashCode * -839892812 + Rotation.GetHashCode();
            hashCode = hashCode * -839892812 + Drops.GetHashCode();
            return hashCode;
        }
    }
}
