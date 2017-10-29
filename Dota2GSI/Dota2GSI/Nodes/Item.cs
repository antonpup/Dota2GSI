﻿namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item information
    /// </summary>
    public class Item : Node
    {
        /// <summary>
        /// Item name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The ID of the player that purchased the item
        /// </summary>
        public readonly int Purchaser;

        /// <summary>
        /// The name of the rune cotnained inside this item.
        /// <note type="note">Possible rune names: empty, arcane, bounty, double_damage, haste, illusion, invisibility, regen</note>
        /// </summary>
        public readonly string ContainsRune;

        /// <summary>
        /// A boolean representing whether this item can be casted
        /// </summary>
        public readonly bool CanCast;

        /// <summary>
        /// Item's cooldown
        /// </summary>
        public readonly int Cooldown;

        /// <summary>
        /// A boolean representing whether this item is passive
        /// </summary>
        public readonly bool IsPassive;

        /// <summary>
        /// The amount of charges on this item
        /// </summary>
        public readonly int Charges;

        internal Item(string json_data) : base(json_data)
        {
            Name = GetString("name");
            Purchaser = GetInt("purchaser");
            ContainsRune = GetString("contains_rune");
            CanCast = GetBool("can_cast");
            Cooldown = GetInt("cooldown");
            IsPassive = GetBool("passive");
            Charges = GetInt("charges");
        }
    }
}
