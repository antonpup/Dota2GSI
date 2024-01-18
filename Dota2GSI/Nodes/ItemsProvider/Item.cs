using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.ItemsProvider
{
    /// <summary>
    /// Enum for runes.
    /// </summary>
    public enum Rune
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// No rune.
        /// </summary>
        Empty,

        /// <summary>
        /// Double Damage rune.
        /// </summary>
        Double_damage,

        /// <summary>
        /// Haste rune.
        /// </summary>
        Haste,

        /// <summary>
        /// Illusion rune.
        /// </summary>
        Illusion,

        /// <summary>
        /// Invisibility rune.
        /// </summary>
        Invis,

        /// <summary>
        /// Regeneration rune.
        /// </summary>
        Regen,

        /// <summary>
        /// Arcane rune.
        /// </summary>
        Arcane,

        /// <summary>
        /// Water.
        /// </summary>
        Water,

        /// <summary>
        /// Bounty rune.
        /// </summary>
        Bounty,

        /// <summary>
        /// Shield rune.
        /// </summary>
        Shield,

        /// <summary>
        /// Experience rune.
        /// </summary>
        XP
    }

    /// <summary>
    /// Class representing item information.
    /// </summary>
    public class Item : Node
    {
        /// <summary>
        /// Item name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The ID of the player that purchased the item.
        /// </summary>
        public readonly int Purchaser;

        /// <summary>
        /// Item level.
        /// </summary>
        public readonly int ItemLevel;

        /// <summary>
        /// The rune contained inside this item.
        /// </summary>
        public readonly Rune ContainsRune;

        /// <summary>
        /// A boolean representing whether this item can be casted.
        /// </summary>
        public readonly bool CanCast;

        /// <summary>
        /// Item's cooldown.
        /// </summary>
        public readonly int Cooldown;

        /// <summary>
        /// A boolean representing whether this item is passive.
        /// </summary>
        public readonly bool IsPassive;

        /// <summary>
        /// Item charges.
        /// <note type="note">Seems to be idential to Charges</note>
        /// </summary>
        public readonly int ItemCharges;

        /// <summary>
        /// The amount of ability charges on this item.
        /// </summary>
        public readonly int AbilityCharges;

        /// <summary>
        /// The amount of maximum charges on this item.
        /// </summary>
        public readonly int MaxCharges;

        /// <summary>
        /// This item's charge cooldown.
        /// </summary>
        public readonly int ChargeCooldown;

        /// <summary>
        /// The amount of charges on this item.
        /// </summary>
        public readonly int Charges;

        internal Item(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            Purchaser = GetInt("purchaser");
            ItemLevel = GetInt("item_level");
            ContainsRune = GetEnum<Rune>("contains_rune");
            CanCast = GetBool("can_cast");
            Cooldown = GetInt("cooldown");
            IsPassive = GetBool("passive");
            ItemCharges = GetInt("item_charges");
            AbilityCharges = GetInt("ability_charges");
            MaxCharges = GetInt("max_charges");
            ChargeCooldown = GetInt("charge_cooldown");
            Charges = GetInt("charges");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"Purchaser: {Purchaser}, " +
                $"ItemLevel: {ItemLevel}, " +
                $"ContainsRune: {ContainsRune}, " +
                $"CanCast: {CanCast}, " +
                $"Cooldown: {Cooldown}, " +
                $"IsPassive: {IsPassive}, " +
                $"ItemCharges: {ItemCharges}, " +
                $"AbilityCharges: {AbilityCharges}, " +
                $"MaxCharges: {MaxCharges}, " +
                $"ChargeCooldown: {ChargeCooldown}, " +
                $"Charges: {Charges}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Item other &&
                Name.Equals(other.Name) &&
                Purchaser.Equals(other.Purchaser) &&
                ItemLevel.Equals(other.ItemLevel) &&
                ContainsRune.Equals(other.ContainsRune) &&
                CanCast.Equals(other.CanCast) &&
                Cooldown.Equals(other.Cooldown) &&
                IsPassive.Equals(other.IsPassive) &&
                ItemCharges.Equals(other.ItemCharges) &&
                AbilityCharges.Equals(other.AbilityCharges) &&
                MaxCharges.Equals(other.MaxCharges) &&
                ChargeCooldown.Equals(other.ChargeCooldown) &&
                Charges.Equals(other.Charges);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 578432725;
            hashCode = hashCode * -280156728 + Name.GetHashCode();
            hashCode = hashCode * -280156728 + Purchaser.GetHashCode();
            hashCode = hashCode * -280156728 + ItemLevel.GetHashCode();
            hashCode = hashCode * -280156728 + ContainsRune.GetHashCode();
            hashCode = hashCode * -280156728 + CanCast.GetHashCode();
            hashCode = hashCode * -280156728 + Cooldown.GetHashCode();
            hashCode = hashCode * -280156728 + IsPassive.GetHashCode();
            hashCode = hashCode * -280156728 + ItemCharges.GetHashCode();
            hashCode = hashCode * -280156728 + AbilityCharges.GetHashCode();
            hashCode = hashCode * -280156728 + MaxCharges.GetHashCode();
            hashCode = hashCode * -280156728 + ChargeCooldown.GetHashCode();
            hashCode = hashCode * -280156728 + Charges.GetHashCode();
            return hashCode;
        }
    }
}
