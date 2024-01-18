using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;
using System.Linq;

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
    /// The hero state.
    /// </summary>
    public enum HeroState
    {
        /// <summary>
        /// No states.
        /// </summary>
        None = 0,

        /// <summary>
        /// Hero is silenced.
        /// </summary>
        Silenced,

        /// <summary>
        /// Hero is stunned.
        /// </summary>
        Stunned,

        /// <summary>
        /// Hero is disarmed.
        /// </summary>
        Disarmed,

        /// <summary>
        /// Hero is magic immune.
        /// </summary>
        MagicImmune,

        /// <summary>
        /// Hero is hexed.
        /// </summary>
        Hexed,

        /// <summary>
        /// Hero is broken.
        /// </summary>
        Broken,

        /// <summary>
        /// Hero is smoked.
        /// </summary>
        Smoked,

        /// <summary>
        /// Hero is debuffed.
        /// </summary>
        Debuffed
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
        /// Current hero state.
        /// </summary>
        public readonly HeroState HeroState;

        /// <summary> 
        /// A boolean representing whether the hero is silenced. 
        /// </summary> 
        public bool IsSilenced => HeroState.HasFlag(HeroState.Silenced);

        /// <summary> 
        /// A boolean representing whether the hero is stunned. 
        /// </summary> 
        public bool IsStunned => HeroState.HasFlag(HeroState.Stunned);

        /// <summary> 
        /// A boolean representing whether the hero is disarmed. 
        /// </summary> 
        public bool IsDisarmed => HeroState.HasFlag(HeroState.Disarmed);

        /// <summary> 
        /// A boolean representing whether the hero is magic immune. 
        /// </summary> 
        public bool IsMagicImmune => HeroState.HasFlag(HeroState.MagicImmune);

        /// <summary> 
        /// A boolean representing whether the hero is hexed. 
        /// </summary> 
        public bool IsHexed => HeroState.HasFlag(HeroState.Hexed);

        /// <summary> 
        /// A boolean representing whether the hero is broken. 
        /// </summary> 
        public bool IsBreak => HeroState.HasFlag(HeroState.Broken);

        /// <summary> 
        /// A boolean representing whether the hero is smoked. 
        /// </summary> 
        public bool IsSmoked => HeroState.HasFlag(HeroState.Smoked);

        /// <summary> 
        /// A boolean representing whether the hero is debuffed. 
        /// </summary> 
        public bool HasDebuff => HeroState.HasFlag(HeroState.Debuffed);

        /// <summary>
        /// A boolean representing whether the hero is muted.
        /// </summary>
        public readonly bool IsMuted;

        /// <summary>
        /// A boolean representing whether the hero has the aghanims scepter upgrade.
        /// </summary>
        public readonly bool HasAghanimsScepterUpgrade;

        /// <summary>
        /// A boolean representing whether the hero has the aghanims shard upgrade.
        /// </summary>
        public readonly bool HasAghanimsShardUpgrade;

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
            IsMuted = GetBool("muted");
            HasAghanimsScepterUpgrade = GetBool("aghanims_scepter");
            HasAghanimsShardUpgrade = GetBool("aghanims_shard");
            SelectedUnit = GetBool("selected_unit");

            HeroState = HeroState.None;

            if (GetBool("silenced"))
            {
                HeroState &= HeroState.Silenced;
            }

            if (GetBool("stunned"))
            {
                HeroState &= HeroState.Stunned;
            }

            if (GetBool("disarmed"))
            {
                HeroState &= HeroState.Disarmed;
            }

            if (GetBool("magicimmune"))
            {
                HeroState &= HeroState.MagicImmune;
            }

            if (GetBool("hexed"))
            {
                HeroState &= HeroState.Hexed;
            }

            if (GetBool("break"))
            {
                HeroState &= HeroState.Broken;
            }

            if (GetBool("smoked"))
            {
                HeroState &= HeroState.Smoked;
            }

            if (GetBool("has_debuff"))
            {
                HeroState &= HeroState.Debuffed;
            }

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
                $"IsSilenced: {HeroState}, " +
                $"IsMuted: {IsMuted}, " +
                $"HasAghanimsScepterUpgrade: {HasAghanimsScepterUpgrade}, " +
                $"HasAghanimsShardUpgrade: {HasAghanimsShardUpgrade}, " +
                $"SelectedUnit: {SelectedUnit}, " +
                $"TalentTree: {TalentTree}, " +
                $"AttributesLevel: {AttributesLevel}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is HeroDetails other &&
                Location.Equals(other.Location) &&
                ID.Equals(other.ID) &&
                Name.Equals(other.Name) &&
                Level.Equals(other.Level) &&
                Experience.Equals(other.Experience) &&
                IsAlive.Equals(other.IsAlive) &&
                SecondsToRespawn.Equals(other.SecondsToRespawn) &&
                BuybackCost.Equals(other.BuybackCost) &&
                BuybackCooldown.Equals(other.BuybackCooldown) &&
                Health.Equals(other.Health) &&
                MaxHealth.Equals(other.MaxHealth) &&
                HealthPercent.Equals(other.HealthPercent) &&
                Mana.Equals(other.Mana) &&
                MaxMana.Equals(other.MaxMana) &&
                ManaPercent.Equals(other.ManaPercent) &&
                IsMuted.Equals(other.IsMuted) &&
                HasAghanimsScepterUpgrade.Equals(other.HasAghanimsScepterUpgrade) &&
                HasAghanimsShardUpgrade.Equals(other.HasAghanimsShardUpgrade) &&
                SelectedUnit.Equals(other.SelectedUnit) &&
                Enumerable.SequenceEqual(TalentTree, other.TalentTree) &&
                AttributesLevel.Equals(other.AttributesLevel);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 189436882;
            hashCode = hashCode * -287957234 + Location.GetHashCode();
            hashCode = hashCode * -287957234 + ID.GetHashCode();
            hashCode = hashCode * -287957234 + Name.GetHashCode();
            hashCode = hashCode * -287957234 + Level.GetHashCode();
            hashCode = hashCode * -287957234 + Experience.GetHashCode();
            hashCode = hashCode * -287957234 + IsAlive.GetHashCode();
            hashCode = hashCode * -287957234 + SecondsToRespawn.GetHashCode();
            hashCode = hashCode * -287957234 + BuybackCost.GetHashCode();
            hashCode = hashCode * -287957234 + BuybackCooldown.GetHashCode();
            hashCode = hashCode * -287957234 + Health.GetHashCode();
            hashCode = hashCode * -287957234 + MaxHealth.GetHashCode();
            hashCode = hashCode * -287957234 + HealthPercent.GetHashCode();
            hashCode = hashCode * -287957234 + Mana.GetHashCode();
            hashCode = hashCode * -287957234 + MaxMana.GetHashCode();
            hashCode = hashCode * -287957234 + ManaPercent.GetHashCode();
            hashCode = hashCode * -287957234 + IsMuted.GetHashCode();
            hashCode = hashCode * -287957234 + HasAghanimsScepterUpgrade.GetHashCode();
            hashCode = hashCode * -287957234 + HasAghanimsShardUpgrade.GetHashCode();
            hashCode = hashCode * -287957234 + SelectedUnit.GetHashCode();
            hashCode = hashCode * -287957234 + TalentTree.GetHashCode();
            hashCode = hashCode * -287957234 + AttributesLevel.GetHashCode();
            return hashCode;
        }
    }
}
