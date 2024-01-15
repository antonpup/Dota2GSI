using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.BuildingsProvider
{
    /// <summary>
    /// Class representing building information.
    /// </summary>
    public class Building : Node
    {
        /// <summary>
        /// Building health.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// Maximum building health.
        /// </summary>
        public readonly int MaxHealth;

        internal Building(JObject parsed_data = null) : base(parsed_data)
        {
            Health = GetInt("health");
            MaxHealth = GetInt("max_health");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Health: {Health}, " +
                $"MaxHealth: {MaxHealth}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Building other &&
                Health == other.Health &&
                MaxHealth == other.MaxHealth;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 815636796;
            hashCode = hashCode * -192822430 + Health.GetHashCode();
            hashCode = hashCode * -192822430 + MaxHealth.GetHashCode();
            return hashCode;
        }
    }
}
