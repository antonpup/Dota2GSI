using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.CouriersProvider
{
    /// <summary>
    /// Class representing a courier.
    /// </summary>
    public class Courier : Node
    {
        /// <summary>
        /// Courier's current health.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// Courier's maximum health.
        /// </summary>
        public readonly int MaxHealth;

        /// <summary>
        /// Is courier alive?
        /// </summary>
        public readonly bool IsAlive;

        /// <summary>
        /// Courier's remaining respawn time.
        /// </summary>
        public readonly int RemainingRespawnTime;

        /// <summary>
        /// Courier's location.
        /// </summary>
        public readonly Vector2D Location;

        /// <summary>
        /// Courier's yaw rotation.
        /// </summary>
        public readonly int Rotation;

        /// <summary>
        /// Courier's owner ID.
        /// </summary>
        public readonly int OwnerID;

        /// <summary>
        /// Does courier have the flying upgrade?
        /// </summary>
        public readonly bool HasFlyingUpgrade;

        /// <summary>
        /// Is courier shielded?
        /// </summary>
        public readonly bool IsShielded;

        /// <summary>
        /// Is courier boosted?
        /// </summary>
        public readonly bool IsBoosted;

        /// <summary>
        /// Items the courier is carrying.
        /// </summary>
        public readonly NodeMap<int, CourierItem> Items = new NodeMap<int, CourierItem>();

        private Regex _item_regex = new Regex(@"item(\d+)");

        internal Courier(JObject parsed_data = null) : base(parsed_data)
        {
            Health = GetInt("health");
            MaxHealth = GetInt("max_health");
            IsAlive = GetBool("alive");
            RemainingRespawnTime = GetInt("respawn_time_remaining");
            Location = new Vector2D(GetInt("xpos"), GetInt("ypos"));
            Rotation = GetInt("yaw");
            OwnerID = GetInt("owner");
            HasFlyingUpgrade = GetBool("flying_upgrade");
            IsShielded = GetBool("shield");
            IsBoosted = GetBool("boost");

            GetMatchingObjects(GetJObject("items"), _item_regex, (Match match, JObject obj) =>
            {
                var item_index = Convert.ToInt32(match.Groups[1].Value);
                var item = new CourierItem(obj);

                Items.Add(item_index, item);
            });
        }

        /// <summary>
        /// Gets the inventory item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The inventory item.</returns>
        public CourierItem GetItemAt(int index)
        {
            if (index < 0 || index > Items.Count - 1)
            {
                return new CourierItem();
            }

            return Items[index];
        }

        /// <summary>
        /// Gets the inventory item by item name.
        /// </summary>
        /// <param name="item_name">The item name to look for.</param>
        /// <returns>The inventory item.</returns>
        public CourierItem GetInventoryItem(string item_name)
        {
            foreach (var item in Items)
            {
                if (item.Value.Name.Equals(item_name))
                {
                    return item.Value;
                }
            }

            return new CourierItem();
        }

        /// <summary>
        /// Checks if item exists in the inventory.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>True if item is in the inventory, false otherwise.</returns>
        public bool InventoryContains(string item_name)
        {
            var found_index = InventoryIndexOf(item_name);
            return found_index > -1;
        }

        /// <summary>
        /// Gets index of the first occurence of the item in the inventory.
        /// </summary>
        /// <param name="item_name">The item name.</param>
        /// <returns>The first index at which item is found, -1 if not found.</returns>
        public int InventoryIndexOf(string item_name)
        {
            foreach (var item_kvp in Items)
            {
                if (item_kvp.Value.Name == item_name)
                {
                    return item_kvp.Key;
                }
            }

            return -1;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Health: {Health}, " +
                $"MaxHealth: {MaxHealth}, " +
                $"IsAlive: {IsAlive}, " +
                $"RemainingRespawnTime: {RemainingRespawnTime}, " +
                $"Location: {Location}, " +
                $"Rotation: {Rotation}, " +
                $"OwnerID: {OwnerID}, " +
                $"HasFlyingUpgrade: {HasFlyingUpgrade}, " +
                $"IsShielded: {IsShielded}, " +
                $"IsBoosted: {IsBoosted}, " +
                $"Items: {Items}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Courier other &&
                Health.Equals(other.Health) &&
                MaxHealth.Equals(other.MaxHealth) &&
                IsAlive.Equals(other.IsAlive) &&
                RemainingRespawnTime.Equals(other.RemainingRespawnTime) &&
                Location.Equals(other.Location) &&
                Rotation.Equals(other.Rotation) &&
                OwnerID.Equals(other.OwnerID) &&
                HasFlyingUpgrade.Equals(other.HasFlyingUpgrade) &&
                IsShielded.Equals(other.IsShielded) &&
                IsBoosted.Equals(other.IsBoosted) &&
                Items.Equals(other.Items);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 232969005;
            hashCode = hashCode * -115989773 + Health.GetHashCode();
            hashCode = hashCode * -115989773 + MaxHealth.GetHashCode();
            hashCode = hashCode * -115989773 + IsAlive.GetHashCode();
            hashCode = hashCode * -115989773 + RemainingRespawnTime.GetHashCode();
            hashCode = hashCode * -115989773 + Location.GetHashCode();
            hashCode = hashCode * -115989773 + Rotation.GetHashCode();
            hashCode = hashCode * -115989773 + OwnerID.GetHashCode();
            hashCode = hashCode * -115989773 + HasFlyingUpgrade.GetHashCode();
            hashCode = hashCode * -115989773 + IsShielded.GetHashCode();
            hashCode = hashCode * -115989773 + IsBoosted.GetHashCode();
            hashCode = hashCode * -115989773 + Items.GetHashCode();
            return hashCode;
        }
    }
}
