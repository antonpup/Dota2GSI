using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.AbilitiesProvider
{
    /// <summary>
    /// Class representing ability information.
    /// </summary>
    public class Ability : Node
    {
        /// <summary>
        /// Ability name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Ability level.
        /// </summary>
        public readonly int Level;

        /// <summary>
        /// A boolean representing whether the ability can be casted.
        /// </summary>
        public readonly bool CanCast;

        /// <summary>
        /// A boolean representing whether the ability is passive.
        /// </summary>
        public readonly bool IsPassive;

        /// <summary>
        /// A boolean representing whether the ability is active.
        /// </summary>
        public readonly bool IsActive;

        /// <summary>
        /// Ability cooldown in seconds.
        /// </summary>
        public readonly int Cooldown;

        /// <summary>
        /// A boolean representing whether the ability is an ultimate.
        /// </summary>
        public readonly bool IsUltimate;

        /// <summary>
        /// Ability charges.
        /// </summary>
        public readonly int Charges;

        /// <summary>
        /// Ability max charges.
        /// </summary>
        public readonly int MaxCharges;

        /// <summary>
        /// Ability charge cooldown.
        /// </summary>
        public readonly int ChargeCooldown;

        internal Ability(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            Level = GetInt("level");
            CanCast = GetBool("can_cast");
            IsPassive = GetBool("passive");
            IsActive = GetBool("ability_active");
            Cooldown = GetInt("cooldown");
            IsUltimate = GetBool("ultimate");
            Charges = GetInt("charges");
            MaxCharges = GetInt("max_charges");
            ChargeCooldown = GetInt("charge_cooldown");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"Level: {Level}, " +
                $"CanCast: {CanCast}, " +
                $"IsPassive: {IsPassive}, " +
                $"IsActive: {IsActive}, " +
                $"Cooldown: {Cooldown}, " +
                $"IsUltimate: {IsUltimate}, " +
                $"Charges: {Charges}, " +
                $"MaxCharges: {MaxCharges}, " +
                $"ChargeCooldown: {ChargeCooldown}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Ability other &&
                Name.Equals(other.Name) &&
                Level.Equals(other.Level) &&
                CanCast.Equals(other.CanCast) &&
                IsPassive.Equals(other.IsPassive) &&
                IsActive.Equals(other.IsActive) &&
                Cooldown.Equals(other.Cooldown) &&
                IsUltimate.Equals(other.IsUltimate) &&
                Charges.Equals(other.Charges) &&
                MaxCharges.Equals(other.MaxCharges) &&
                ChargeCooldown.Equals(other.ChargeCooldown);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 634642291;
            hashCode = hashCode * -84013849 + Name.GetHashCode();
            hashCode = hashCode * -84013849 + Level.GetHashCode();
            hashCode = hashCode * -84013849 + CanCast.GetHashCode();
            hashCode = hashCode * -84013849 + IsPassive.GetHashCode();
            hashCode = hashCode * -84013849 + IsActive.GetHashCode();
            hashCode = hashCode * -84013849 + Cooldown.GetHashCode();
            hashCode = hashCode * -84013849 + IsUltimate.GetHashCode();
            hashCode = hashCode * -84013849 + Charges.GetHashCode();
            hashCode = hashCode * -84013849 + MaxCharges.GetHashCode();
            hashCode = hashCode * -84013849 + ChargeCooldown.GetHashCode();
            return hashCode;
        }
    }
}
