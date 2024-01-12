using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing item drops.
    /// </summary>
    public class ItemsDrop : Node
    {
        /// <summary>
        /// Items that can drop.
        /// </summary>
        public readonly Dictionary<int, string> Items = new Dictionary<int, string>();

        private Regex _item_regex = new Regex(@"item(\d+)");
        internal ItemsDrop(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingStrings(parsed_data, _item_regex, (Match match, string value) =>
            {
                var item_index = Convert.ToInt32(match.Groups[1].Value);

                Items.Add(item_index, value);
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Items: {Items}" +
                $"]";
        }
    }

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
    }
}
