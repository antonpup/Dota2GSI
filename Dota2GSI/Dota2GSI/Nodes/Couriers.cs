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

        public CourierItem(JObject parsed_data = null) : base(parsed_data)
        {
            OwnerID = GetInt("owner");
            Name = GetString("name");
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

        public Courier(JObject parsed_data = null) : base(parsed_data)
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

            var items = GetJObject("items");
            if (items != null)
            {
                foreach (var property in items.Properties())
                {
                    string property_name = property.Name;

                    if (_item_regex.IsMatch(property_name))
                    {
                        var match = _item_regex.Match(property_name);
                        var item_index = Convert.ToInt32(match.Groups[1].Value);
                        var item = new CourierItem(property.Value as JObject);

                        Items.Add(item_index, item);
                    }
                }
            }
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
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_courier_regex.IsMatch(property_name) && property.Value.Type == JTokenType.Object)
                    {
                        var match = _courier_regex.Match(property_name);
                        var item_index = Convert.ToInt32(match.Groups[1].Value);
                        var courier = new Courier(property.Value as JObject);

                        CouriersMap.Add(item_index, courier);
                    }
                }
            }
        }
    }
}
