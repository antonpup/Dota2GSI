using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
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
        public readonly Dictionary<int, int> KillList = new Dictionary<int, int>();

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
    }

    /// <summary>
    /// Class representing player information.
    /// </summary>
    public class Player : Node
    {
        /// <summary>
        /// The local player's details.
        /// </summary>
        public readonly PlayerDetails LocalPlayer;

        /// <summary>
        /// The team player's details. (SPECTATOR ONLY)
        /// </summary>
        public readonly Dictionary<PlayerTeam, Dictionary<int, PlayerDetails>> Teams = new Dictionary<PlayerTeam, Dictionary<int, PlayerDetails>>();

        private Regex _team_id_regex = new Regex(@"team(\d+)");
        private Regex _player_id_regex = new Regex(@"player(\d+)");

        internal Player(JObject parsed_data = null) : base(parsed_data)
        {
            // Attempt to parse the local player details
            LocalPlayer = new PlayerDetails(parsed_data);

            // Attempt to parse team player details
            GetMatchingObjects(parsed_data, _team_id_regex, (Match match, JObject obj) =>
            {
                var team_id = (PlayerTeam)Convert.ToInt32(match.Groups[1].Value);

                if (!Teams.ContainsKey(team_id))
                {
                    Teams.Add(team_id, new Dictionary<int, PlayerDetails>());
                }

                GetMatchingObjects(obj, _player_id_regex, (Match sub_match, JObject sub_obj) =>
                {
                    var player_index = Convert.ToInt32(sub_match.Groups[1].Value);
                    var player_details = new PlayerDetails(sub_obj);

                    if (!Teams[team_id].ContainsKey(player_index))
                    {
                        Teams[team_id].Add(player_index, player_details);
                    }
                    else
                    {
                        Teams[team_id][player_index] = player_details;
                    }
                });
            });
        }

        /// <summary>
        /// Gets the player details for a specific team.
        /// </summary>
        /// <param name="team_id">The team.</param>
        /// <returns>A dictionary of player id mapped to their player details.</returns>
        public Dictionary<int, PlayerDetails> GetTeam(PlayerTeam team)
        {
            if (Teams.ContainsKey(team))
            {
                return Teams[team];
            }

            return new Dictionary<int, PlayerDetails>();
        }

        /// <summary>
        /// Gets the player details for a specific player.
        /// </summary>
        /// <param name="player_id">The player id.</param>
        /// <returns>The player details.</returns>
        public PlayerDetails GetPlayer(int player_id)
        {
            foreach (var team in Teams)
            {
                foreach (var player in team.Value)
                {
                    if (player.Key == player_id)
                    {
                        return player.Value;
                    }
                }
            }

            return new PlayerDetails();
        }
    }
}
