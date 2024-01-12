using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing a courier item.
    /// </summary>
    public class CourierItem : Node
    {
        /// <summary>
        /// Item's owner ID.
        /// </summary>
        public readonly int OwnerID;

        /// <summary>
        /// Item's name.
        /// </summary>
        public readonly string Name;

        internal CourierItem(JObject parsed_data = null) : base(parsed_data)
        {
            OwnerID = GetInt("owner");
            Name = GetString("name");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"OwnerID: {OwnerID}" +
                $"]";
        }
    }

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

    /// <summary>
    /// Class representing couriers.
    /// </summary>
    public class Couriers : Node
    {
        /// <summary>
        /// A dictionary mapping of courier ID to courier.
        /// </summary>
        public readonly Dictionary<int, Courier> CouriersMap = new Dictionary<int, Courier>();

        private Regex _courier_regex = new Regex(@"courier(\d+)");

        internal Couriers(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingObjects(parsed_data, _courier_regex, (Match match, JObject obj) =>
            {
                var item_index = Convert.ToInt32(match.Groups[1].Value);
                var courier = new Courier(obj);

                CouriersMap.Add(item_index, courier);
            });
        }

        /// <summary>
        /// Gets the courier for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The courier.</returns>
        public Courier GetForPlayer(int player_id)
        {
            foreach (var courier in CouriersMap)
            {
                if (courier.Value.OwnerID == player_id)
                {
                    return courier.Value;
                }
            }

            return new Courier();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"CouriersMap: {CouriersMap}" +
                $"]";
        }
    }
}
