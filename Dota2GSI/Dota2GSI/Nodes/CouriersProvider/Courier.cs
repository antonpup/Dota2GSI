using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public readonly Dictionary<int, CourierItem> Items = new Dictionary<int, CourierItem>();

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
    }
}
