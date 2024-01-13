using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.HeroProvider
{
    /// <summary>
    /// The talent tree selection.
    /// </summary>
    public enum TalentTreeSpec
    {
        /// <summary>
        /// Nothing has been selected at this tier.
        /// </summary>
        None = 0,

        /// <summary>
        /// The left side of the tree has been selected at this tier.
        /// </summary>
        Left,

        /// <summary>
        /// The right side of the tree has been selected at this tier.
        /// </summary>
        Right,

        /// <summary>
        /// Both sides of the tree have been selected at this tier.
        /// </summary>
        Both
    }

    /// <summary>
    /// Class representing hero details.
    /// </summary>
    public class HeroDetails : Node
    {
        /// <summary>
        /// Location of the Hero on the map.
        /// </summary>
        public readonly Vector2D Location;

        /// <summary>
        /// Hero ID.
        /// </summary>
        public readonly int ID;

        /// <summary>
        /// Hero name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Hero level.
        /// </summary>
        public readonly int Level;

        /// <summary>
        /// Hero experience.
        /// </summary>
        public readonly int Experience;

        /// <summary>
        /// A boolean representing whether the hero is alive.
        /// </summary>
        public readonly bool IsAlive;

        /// <summary>
        /// Amount of seconds until the hero respawns.
        /// </summary>
        public readonly int SecondsToRespawn;

        /// <summary>
        /// The buyback cost.
        /// </summary>
        public readonly int BuybackCost;

        /// <summary>
        /// The buyback cooldown.
        /// </summary>
        public readonly int BuybackCooldown;

        /// <summary>
        /// Hero current health.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// Hero max health.
        /// </summary>
        public readonly int MaxHealth;

        /// <summary>
        /// Hero health percentage.
        /// </summary>
        public readonly int HealthPercent;

        /// <summary>
        /// Hero current mana.
        /// </summary>
        public readonly int Mana;

        /// <summary>
        /// Hero max mana.
        /// </summary>
        public readonly int MaxMana;

        /// <summary>
        /// Hero mana percent.
        /// </summary>
        public readonly int ManaPercent;

        /// <summary>
        /// A boolean representing whether the hero is silenced.
        /// </summary>
        public readonly bool IsSilenced;

        /// <summary>
        /// A boolean representing whether the hero is stunned.
        /// </summary>
        public readonly bool IsStunned;

        /// <summary>
        /// A boolean representing whether the hero is disarmed.
        /// </summary>
        public readonly bool IsDisarmed;

        /// <summary>
        /// A boolean representing whether the hero is magic immune.
        /// </summary>
        public readonly bool IsMagicImmune;

        /// <summary>
        /// A boolean representing whether the hero is hexed.
        /// </summary>
        public readonly bool IsHexed;

        /// <summary>
        /// A boolean representing whether the hero is muted.
        /// </summary>
        public readonly bool IsMuted;

        /// <summary>
        /// A boolean representing whether the hero is broken.
        /// </summary>
        public readonly bool IsBreak;

        /// <summary>
        /// A boolean representing whether the hero has the aghanims scepter upgrade.
        /// </summary>
        public readonly bool HasAghanimsScepterUpgrade;

        /// <summary>
        /// A boolean representing whether the hero has the aghanims shard upgrade.
        /// </summary>
        public readonly bool HasAghanimsShardUpgrade;

        /// <summary>
        /// A boolean representing whether the hero is smoked.
        /// </summary>
        public readonly bool IsSmoked;

        /// <summary>
        /// A boolean representing whether the hero is debuffed.
        /// </summary>
        public readonly bool HasDebuff;

        /// <summary>
        /// A boolean representing whether this hero is currently selected by the spectator. (SPECTATOR ONLY)
        /// </summary>
        public readonly bool SelectedUnit;

        /// <summary>
        /// The chosen talents for the Hero. Starts at the bottom.
        /// </summary>
        public readonly TalentTreeSpec[] TalentTree;

        /// <summary>
        /// Hero attributes level.
        /// </summary>
        public readonly int AttributesLevel;

        internal HeroDetails(JObject parsed_data = null) : base(parsed_data)
        {
            Location = new Vector2D(GetInt("xpos"), GetInt("ypos"));
            ID = GetInt("id");
            Name = GetString("name");
            Level = GetInt("level");
            Experience = GetInt("xp");
            IsAlive = GetBool("alive");
            SecondsToRespawn = GetInt("respawn_seconds");
            BuybackCost = GetInt("buyback_cost");
            BuybackCooldown = GetInt("buyback_cooldown");
            Health = GetInt("health");
            MaxHealth = GetInt("max_health");
            HealthPercent = GetInt("health_percent");
            Mana = GetInt("mana");
            MaxMana = GetInt("max_mana");
            ManaPercent = GetInt("mana_percent");
            IsSilenced = GetBool("silenced");
            IsStunned = GetBool("stunned");
            IsDisarmed = GetBool("disarmed");
            IsMagicImmune = GetBool("magicimmune");
            IsHexed = GetBool("hexed");
            IsMuted = GetBool("muted");
            IsBreak = GetBool("break");
            HasAghanimsScepterUpgrade = GetBool("aghanims_scepter");
            HasAghanimsShardUpgrade = GetBool("aghanims_shard");
            IsSmoked = GetBool("smoked");
            HasDebuff = GetBool("has_debuff");
            SelectedUnit = GetBool("selected_unit");

            TalentTree = new TalentTreeSpec[4];
            for (int i = 0; i < TalentTree.Length; i++)
            {
                TalentTree[i] = TalentTreeSpec.None;
            }

            for (int i = 1; i <= 4; i++)
            {
                int left_index = (i * 2);
                int right_index = (i * 2) - 1;

                bool left_taken = GetBool("talent_" + left_index);
                bool right_taken = GetBool("talent_" + right_index);

                if (left_taken && !right_taken)
                {
                    TalentTree[i - 1] = TalentTreeSpec.Left;
                }
                else if (!left_taken && right_taken)
                {
                    TalentTree[i - 1] = TalentTreeSpec.Right;
                }
                else if (left_taken && right_taken)
                {
                    TalentTree[i - 1] = TalentTreeSpec.Both;
                }
            }

            AttributesLevel = GetInt("attributes_level");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Location: {Location}, " +
                $"ID: {ID}, " +
                $"Name: {Name}, " +
                $"Level: {Level}, " +
                $"Experience: {Experience}, " +
                $"IsAlive: {IsAlive}, " +
                $"SecondsToRespawn: {SecondsToRespawn}, " +
                $"BuybackCost: {BuybackCost}, " +
                $"BuybackCooldown: {BuybackCooldown}, " +
                $"Health: {Health}, " +
                $"MaxHealth: {MaxHealth}, " +
                $"HealthPercent: {HealthPercent}, " +
                $"Mana: {Mana}, " +
                $"MaxMana: {MaxMana}, " +
                $"ManaPercent: {ManaPercent}, " +
                $"IsSilenced: {IsSilenced}, " +
                $"IsStunned: {IsStunned}, " +
                $"IsDisarmed: {IsDisarmed}, " +
                $"IsMagicImmune: {IsMagicImmune}, " +
                $"IsHexed: {IsHexed}, " +
                $"IsMuted: {IsMuted}, " +
                $"IsBreak: {IsBreak}, " +
                $"HasAghanimsScepterUpgrade: {HasAghanimsScepterUpgrade}, " +
                $"HasAghanimsShardUpgrade: {HasAghanimsShardUpgrade}, " +
                $"IsSmoked: {IsSmoked}, " +
                $"HasDebuff: {HasDebuff}, " +
                $"SelectedUnit: {SelectedUnit}, " +
                $"TalentTree: {TalentTree}, " +
                $"AttributesLevel: {AttributesLevel}" +
                $"]";
        }
    }
}
