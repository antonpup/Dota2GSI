using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Enum for various player activities
    /// </summary>
    public enum PlayerActivity
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// In a menu
        /// </summary>
        Menu,

        /// <summary>
        /// In a game
        /// </summary>
        Playing
    }

    /// <summary>
    /// Class representing player information
    /// </summary>
    public class Player : Node
    {
        /// <summary>
        /// Player's steam ID
        /// </summary>
        public readonly string SteamID;

        /// <summary>
        /// Player's name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Player's pro name
        /// </summary>
        public readonly string ProName;

        /// <summary>
        /// Player's current activity state
        /// </summary>
        public readonly PlayerActivity Activity;

        /// <summary>
        /// Player's amount of kills
        /// </summary>
        public readonly int Kills;

        /// <summary>
        /// Player's amount of deaths
        /// </summary>
        public readonly int Deaths;

        /// <summary>
        /// Player's amount of assists
        /// </summary>
        public readonly int Assists;

        /// <summary>
        /// Player's amount of last hits
        /// </summary>
        public readonly int LastHits;

        /// <summary>
        /// Player's amount of denies
        /// </summary>
        public readonly int Denies;

        /// <summary>
        /// Player's killstreak
        /// </summary>
        public readonly int KillStreak;

        /// <summary>
        /// Commands issued by the player
        /// </summary>
        public readonly int CommandsIssued;

        /// <summary>
        /// Player's list of kills. The index corresponds to the player no which can be used to find the playerdetails if in spectator mode using the Teams.AllPlayers property
        /// </summary>
        public readonly Dictionary<int, int> KillList;

        /// <summary>
        /// Player's team
        /// </summary>
        public readonly PlayerTeam Team;

        /// <summary>
        /// Player's amount of gold
        /// </summary>
        public readonly int Gold;

        /// <summary>
        /// Player's amount of reliable gold
        /// </summary>
        public readonly int GoldReliable;

        /// <summary>
        /// Player's amount of unreliable gold
        /// </summary>
        public readonly int GoldUnreliable;

        /// <summary>
        /// PLayer's gold per minute
        /// </summary>
        public readonly int GoldPerMinute;

        /// <summary>
        /// Player's experience per minute
        /// </summary>
        public readonly int ExperiencePerMinute;

        /// <summary>
        /// Player's net worth (SPECTATOR ONLY)
        /// </summary>
        public readonly int NetWorth;

        /// <summary>
        /// Player's hero damage (SPECTATOR ONLY)
        /// </summary>
        public readonly int HeroDamage;

        /// <summary>
        /// Player's gold spent on support items (SPECTATOR ONLY)
        /// </summary>
        public readonly int SupportGoldSpent;

        /// <summary>
        /// The amount of wards the player has purchased (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsPurchased;

        /// <summary>
        /// The amount of wards placed by the player (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsPlaced;

        /// <summary>
        /// The amount of wards destroyed by the player (SPECTATOR ONLY)
        /// </summary>
        public readonly int WardsDestroyed;

        /// <summary>
        /// The amount of runes activated by the player (SPECTATOR ONLY)
        /// </summary>
        public readonly int RunesActivated;

        /// <summary>
        /// The amount of camps stacked by the player (SPECTATOR ONLY)
        /// </summary>
        public readonly int CampsStacked;

        internal Player(string json_data) : base(json_data)
        {
            SteamID = GetString("steamid");
            Name = GetString("name");
            ProName = GetString("pro_name");
            Activity = GetEnum<PlayerActivity>("activity");
            Kills = GetInt("kills");
            Deaths = GetInt("deaths");
            Assists = GetInt("assists");
            LastHits = GetInt("last_hits");
            Denies = GetInt("denies");
            KillStreak = GetInt("kill_streak");
            CommandsIssued = GetInt("commands_issued");

            this.KillList = new Dictionary<int, int>();
            foreach(JValue kill in GetArray("kill_list"))
            {
                int id;
                if (int.TryParse(kill.Path.Replace("kill_list.victimid_", ""), out id))
                {
                    this.KillList.Add(id, kill.Value<int>());
                }
                else
                {
                    System.Console.WriteLine("[DOTA2GSI] Warning, could not get victim ID! ID: " + kill.Path);
                }
            }

            Team = GetEnum<PlayerTeam>("team_name");
            Gold = GetInt("gold");
            GoldReliable = GetInt("gold_reliable");
            GoldUnreliable = GetInt("gold_unreliable");
            GoldPerMinute = GetInt("gpm");
            ExperiencePerMinute = GetInt("xpm");

            NetWorth = GetInt("net_worth");
            HeroDamage = GetInt("hero_damage");
            SupportGoldSpent = GetInt("support_gold_spent");
            WardsPurchased = GetInt("wards_purchased");
            WardsPlaced = GetInt("wards_placed");
            WardsDestroyed = GetInt("wards_destroyed");
            RunesActivated = GetInt("runes_activated");
            CampsStacked = GetInt("camps_stacked");
        }
    }
}
