using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.PlayerProvider
{
    /// <summary>
    /// Enum for various player activities.
    /// </summary>
    public enum PlayerActivity
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// In a menu.
        /// </summary>
        Menu,

        /// <summary>
        /// In a game.
        /// </summary>
        Playing
    }

    /// <summary>
    /// Class representing player details.
    /// </summary>
    public class PlayerDetails : Node
    {
        /// <summary>
        /// Player's steam ID.
        /// </summary>
        public readonly string SteamID;

        /// <summary>
        /// Player's account ID.
        /// </summary>
        public readonly string AccountID;

        /// <summary>
        /// Player's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Player's current activity state.
        /// </summary>
        public readonly PlayerActivity Activity;

        /// <summary>
        /// Player's amount of kills.
        /// </summary>
        public readonly int Kills;

        /// <summary>
        /// Player's amount of deaths.
        /// </summary>
        public readonly int Deaths;

        /// <summary>
        /// Player's amount of assists.
        /// </summary>
        public readonly int Assists;

        /// <summary>
        /// Player's amount of last hits.
        /// </summary>
        public readonly int LastHits;

        /// <summary>
        /// Player's amount of denies.
        /// </summary>
        public readonly int Denies;

        /// <summary>
        /// Player's killstreak.
        /// </summary>
        public readonly int KillStreak;

        /// <summary>
        /// Commands issued by the player.
        /// </summary>
        public readonly int CommandsIssued;

        /// <summary>
        /// Player's list of kills. The index corresponds to the player no which can be used to find the playerdetails if in spectator mode using the Teams.AllPlayers property.
        /// </summary>
        public readonly NodeMap<int, int> KillList = new NodeMap<int, int>();

        /// <summary>
        /// Player's team.
        /// </summary>
        public readonly PlayerTeam Team;

        /// <summary>
        /// Player's slot.
        /// </summary>
        public readonly int PlayerSlot;

        /// <summary>
        /// Player's team slot.
        /// </summary>
        public readonly int PlayerTeamSlot;

        /// <summary>
        /// Player's amount of gold.
        /// </summary>
        public readonly int Gold;

        /// <summary>
        /// Player's amount of reliable gold.
        /// </summary>
        public readonly int GoldReliable;

        /// <summary>
        /// Player's amount of unreliable gold.
        /// </summary>
        public readonly int GoldUnreliable;

        /// <summary>
        /// Player's amount of gold earned from hero kills.
        /// </summary>
        public readonly int GoldFromHeroKills;

        /// <summary>
        /// Player's amount of gold earned from creep kills.
        /// </summary>
        public readonly int GoldFromCreepKills;

        /// <summary>
        /// Player's amount of gold earned from passive income.
        /// </summary>
        public readonly int GoldFromIncome;

        /// <summary>
        /// Player's amount of gold earned from shared.
        /// </summary>
        public readonly int GoldFromShared;

        /// <summary>
        /// PLayer's gold per minute.
        /// </summary>
        public readonly int GoldPerMinute;

        /// <summary>
        /// Player's experience per minute.
        /// </summary>
        public readonly int ExperiencePerMinute;

        /// <summary>
        /// Player's onstage seat.
        /// </summary>
        public readonly int OnstageSeat;

        /// <summary>
        /// Player's net worth. (SPECTATOR ONLY)
        /// </summary>
        public readonly int NetWorth;

        /// <summary>
        /// Player's hero damage. (SPECTATOR ONLY)
        /// </summary>
        public readonly int HeroDamage;

        /// <summary>
        /// Player's hero healing. (SPECTATOR ONLY)
        /// </summary>
        public readonly int HeroHealing;

        /// <summary>
        /// Player's tower damage. (SPECTATOR ONLY)
        /// </summary>
        public readonly int TowerDamage;

        /// <summary>
        /// Player's gold spent on support items. (SPECTATOR ONLY)
        /// </summary>
        public readonly int SupportGoldSpent;

        /// <summary>
        /// Player's gold spent on consumable items. (SPECTATOR ONLY)
        /// </summary>
        public readonly int ConsumableGoldSpent;

        /// <summary>
        /// Player's gold spent on items. (SPECTATOR ONLY)
        /// </summary>
        public readonly int ItemGoldSpent;

        /// <summary>
        /// Player's gold lost to deaths. (SPECTATOR ONLY)
        /// </summary>
        public readonly int GoldLostToDeath;

        /// <summary>
        /// Player's gold spent on buybacks. (SPECTATOR ONLY)
        /// </summary>
        public readonly int GoldSpentOnBuybacks;

        /// <summary>
        /// The amount of wards the player has purchased. (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsPurchased;

        /// <summary>
        /// The amount of wards placed by the player. (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsPlaced;

        /// <summary>
        /// The amount of wards destroyed by the player. (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsDestroyed;

        /// <summary>
        /// The amount of runes activated by the player. (SPECTATOR ONLY)
        /// </summary>
        public readonly int RunesActivated;

        /// <summary>
        /// The amount of camps stacked by the player. (SPECTATOR ONLY)
        /// </summary>
        public readonly int CampsStacked;

        private Regex _victim_id_regex = new Regex(@"victimid_(\d+)");

        internal PlayerDetails(JObject parsed_data = null) : base(parsed_data)
        {
            SteamID = GetString("steamid");
            AccountID = GetString("accountid");
            Name = GetString("name");
            Activity = GetEnum<PlayerActivity>("activity");
            Kills = GetInt("kills");
            Deaths = GetInt("deaths");
            Assists = GetInt("assists");
            LastHits = GetInt("last_hits");
            Denies = GetInt("denies");
            KillStreak = GetInt("kill_streak");
            CommandsIssued = GetInt("commands_issued");

            GetMatchingIntegers(GetJObject("kill_list"), _victim_id_regex, (Match match, int value) =>
            {
                int kill_id = Convert.ToInt32(match.Groups[1].Value);

                KillList.Add(kill_id, value);
            });

            Team = GetEnum<PlayerTeam>("team_name");
            PlayerSlot = GetInt("player_slot");
            PlayerTeamSlot = GetInt("team_slot");
            Gold = GetInt("gold");
            GoldReliable = GetInt("gold_reliable");
            GoldUnreliable = GetInt("gold_unreliable");
            GoldFromHeroKills = GetInt("gold_from_hero_kills");
            GoldFromCreepKills = GetInt("gold_from_creep_kills");
            GoldFromIncome = GetInt("gold_from_income");
            GoldFromShared = GetInt("gold_from_shared");
            GoldPerMinute = GetInt("gpm");
            ExperiencePerMinute = GetInt("xpm");
            OnstageSeat = GetInt("onstage_seat");
            NetWorth = GetInt("net_worth");
            HeroDamage = GetInt("hero_damage");
            HeroHealing = GetInt("hero_healing");
            TowerDamage = GetInt("tower_damage");
            WardsPurchased = GetInt("wards_purchased");
            WardsPlaced = GetInt("wards_placed");
            WardsDestroyed = GetInt("wards_destroyed");
            RunesActivated = GetInt("runes_activated");
            CampsStacked = GetInt("camps_stacked");
            SupportGoldSpent = GetInt("support_gold_spent");
            ConsumableGoldSpent = GetInt("consumable_gold_spent");
            ItemGoldSpent = GetInt("item_gold_spent");
            GoldLostToDeath = GetInt("gold_lost_to_death");
            GoldSpentOnBuybacks = GetInt("gold_spent_on_buybacks");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"SteamID: {SteamID}, " +
                $"AccountID: {AccountID}, " +
                $"Name: {Name}, " +
                $"Activity: {Activity}, " +
                $"Kills: {Kills}, " +
                $"Deaths: {Deaths}, " +
                $"Assists: {Assists}, " +
                $"LastHits: {LastHits}, " +
                $"Denies: {Denies}, " +
                $"KillStreak: {KillStreak}, " +
                $"CommandsIssued: {CommandsIssued}, " +
                $"KillList: {KillList}, " +
                $"Team: {Team}, " +
                $"PlayerSlot: {PlayerSlot}, " +
                $"PlayerTeamSlot: {PlayerTeamSlot}, " +
                $"Gold: {Gold}, " +
                $"GoldReliable: {GoldReliable}, " +
                $"GoldUnreliable: {GoldUnreliable}, " +
                $"GoldFromHeroKills: {GoldFromHeroKills}, " +
                $"GoldFromCreepKills: {GoldFromCreepKills}, " +
                $"GoldFromIncome: {GoldFromIncome}, " +
                $"GoldFromShared: {GoldFromShared}, " +
                $"GoldPerMinute: {GoldPerMinute}, " +
                $"ExperiencePerMinute: {ExperiencePerMinute}, " +
                $"OnstageSeat: {OnstageSeat}, " +
                $"NetWorth: {NetWorth}, " +
                $"HeroDamage: {HeroDamage}, " +
                $"HeroHealing: {HeroHealing}, " +
                $"TowerDamage: {TowerDamage}, " +
                $"SupportGoldSpent: {SupportGoldSpent}, " +
                $"ConsumableGoldSpent: {ConsumableGoldSpent}, " +
                $"ItemGoldSpent: {ItemGoldSpent}, " +
                $"GoldLostToDeath: {GoldLostToDeath}, " +
                $"GoldSpentOnBuybacks: {GoldSpentOnBuybacks}, " +
                $"WardsPurchased: {WardsPurchased}, " +
                $"WardsPlaced: {WardsPlaced}, " +
                $"WardsDestroyed: {WardsDestroyed}, " +
                $"RunesActivated: {RunesActivated}, " +
                $"CampsStacked: {CampsStacked}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is PlayerDetails other &&
                SteamID.Equals(other.SteamID) &&
                AccountID.Equals(other.AccountID) &&
                Name.Equals(other.Name) &&
                Activity.Equals(other.Activity) &&
                Kills.Equals(other.Kills) &&
                Deaths.Equals(other.Deaths) &&
                Assists.Equals(other.Assists) &&
                LastHits.Equals(other.LastHits) &&
                Denies.Equals(other.Denies) &&
                KillStreak.Equals(other.KillStreak) &&
                CommandsIssued.Equals(other.CommandsIssued) &&
                KillList.Equals(other.KillList) &&
                Team.Equals(other.Team) &&
                PlayerSlot.Equals(other.PlayerSlot) &&
                PlayerTeamSlot.Equals(other.PlayerTeamSlot) &&
                Gold.Equals(other.Gold) &&
                GoldReliable.Equals(other.GoldReliable) &&
                GoldUnreliable.Equals(other.GoldUnreliable) &&
                GoldFromHeroKills.Equals(other.GoldFromHeroKills) &&
                GoldFromCreepKills.Equals(other.GoldFromCreepKills) &&
                GoldFromIncome.Equals(other.GoldFromIncome) &&
                GoldFromShared.Equals(other.GoldFromShared) &&
                GoldPerMinute.Equals(other.GoldPerMinute) &&
                ExperiencePerMinute.Equals(other.ExperiencePerMinute) &&
                OnstageSeat.Equals(other.OnstageSeat) &&
                NetWorth.Equals(other.NetWorth) &&
                HeroDamage.Equals(other.HeroDamage) &&
                HeroHealing.Equals(other.HeroHealing) &&
                TowerDamage.Equals(other.TowerDamage) &&
                SupportGoldSpent.Equals(other.SupportGoldSpent) &&
                ConsumableGoldSpent.Equals(other.ConsumableGoldSpent) &&
                ItemGoldSpent.Equals(other.ItemGoldSpent) &&
                GoldLostToDeath.Equals(other.GoldLostToDeath) &&
                GoldSpentOnBuybacks.Equals(other.GoldSpentOnBuybacks) &&
                WardsPurchased.Equals(other.WardsPurchased) &&
                WardsPlaced.Equals(other.WardsPlaced) &&
                WardsDestroyed.Equals(other.WardsDestroyed) &&
                RunesActivated.Equals(other.RunesActivated) &&
                CampsStacked.Equals(other.CampsStacked);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 602668635;
            hashCode = hashCode * -112730515 + SteamID.GetHashCode();
            hashCode = hashCode * -112730515 + AccountID.GetHashCode();
            hashCode = hashCode * -112730515 + Name.GetHashCode();
            hashCode = hashCode * -112730515 + Activity.GetHashCode();
            hashCode = hashCode * -112730515 + Kills.GetHashCode();
            hashCode = hashCode * -112730515 + Deaths.GetHashCode();
            hashCode = hashCode * -112730515 + Assists.GetHashCode();
            hashCode = hashCode * -112730515 + LastHits.GetHashCode();
            hashCode = hashCode * -112730515 + Denies.GetHashCode();
            hashCode = hashCode * -112730515 + KillStreak.GetHashCode();
            hashCode = hashCode * -112730515 + CommandsIssued.GetHashCode();
            hashCode = hashCode * -112730515 + KillList.GetHashCode();
            hashCode = hashCode * -112730515 + Team.GetHashCode();
            hashCode = hashCode * -112730515 + PlayerSlot.GetHashCode();
            hashCode = hashCode * -112730515 + PlayerTeamSlot.GetHashCode();
            hashCode = hashCode * -112730515 + Gold.GetHashCode();
            hashCode = hashCode * -112730515 + GoldReliable.GetHashCode();
            hashCode = hashCode * -112730515 + GoldUnreliable.GetHashCode();
            hashCode = hashCode * -112730515 + GoldFromHeroKills.GetHashCode();
            hashCode = hashCode * -112730515 + GoldFromCreepKills.GetHashCode();
            hashCode = hashCode * -112730515 + GoldFromIncome.GetHashCode();
            hashCode = hashCode * -112730515 + GoldFromShared.GetHashCode();
            hashCode = hashCode * -112730515 + GoldPerMinute.GetHashCode();
            hashCode = hashCode * -112730515 + ExperiencePerMinute.GetHashCode();
            hashCode = hashCode * -112730515 + OnstageSeat.GetHashCode();
            hashCode = hashCode * -112730515 + NetWorth.GetHashCode();
            hashCode = hashCode * -112730515 + HeroDamage.GetHashCode();
            hashCode = hashCode * -112730515 + HeroHealing.GetHashCode();
            hashCode = hashCode * -112730515 + TowerDamage.GetHashCode();
            hashCode = hashCode * -112730515 + SupportGoldSpent.GetHashCode();
            hashCode = hashCode * -112730515 + ConsumableGoldSpent.GetHashCode();
            hashCode = hashCode * -112730515 + ItemGoldSpent.GetHashCode();
            hashCode = hashCode * -112730515 + GoldLostToDeath.GetHashCode();
            hashCode = hashCode * -112730515 + GoldSpentOnBuybacks.GetHashCode();
            hashCode = hashCode * -112730515 + WardsPurchased.GetHashCode();
            hashCode = hashCode * -112730515 + WardsPlaced.GetHashCode();
            hashCode = hashCode * -112730515 + WardsDestroyed.GetHashCode();
            hashCode = hashCode * -112730515 + RunesActivated.GetHashCode();
            hashCode = hashCode * -112730515 + CampsStacked.GetHashCode();
            return hashCode;
        }
    }
}
