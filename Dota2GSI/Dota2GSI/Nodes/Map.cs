using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Enum list for each Game State.
    /// </summary>
    public enum DOTA_GameState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Disconnected.
        /// </summary>
        DOTA_GAMERULES_STATE_DISCONNECT,

        /// <summary>
        /// Game is in progress.
        /// </summary>
        DOTA_GAMERULES_STATE_GAME_IN_PROGRESS,

        /// <summary>
        /// Players are currently selecting heroes.
        /// </summary>
        DOTA_GAMERULES_STATE_HERO_SELECTION,

        /// <summary>
        /// Game is starting.
        /// </summary>
        DOTA_GAMERULES_STATE_INIT,

        /// <summary>
        /// Game is ending.
        /// </summary>
        DOTA_GAMERULES_STATE_LAST,

        /// <summary>
        /// Game has ended, post game scoreboard.
        /// </summary>
        DOTA_GAMERULES_STATE_POST_GAME,

        /// <summary>
        /// Game has started, pre game preparations.
        /// </summary>
        DOTA_GAMERULES_STATE_PRE_GAME,

        /// <summary>
        /// Players are selecting/banning heroes.
        /// </summary>
        DOTA_GAMERULES_STATE_STRATEGY_TIME,

        /// <summary>
        /// Waiting for everyone to connect and load.
        /// </summary>
        DOTA_GAMERULES_STATE_WAIT_FOR_PLAYERS_TO_LOAD,

        /// <summary>
        /// Game is a custom game.
        /// </summary>
        DOTA_GAMERULES_STATE_CUSTOM_GAME_SETUP,

        /// <summary>
        /// Waiting for the map to load.
        /// </summary>
        DOTA_GAMERULES_STATE_WAIT_FOR_MAP_TO_LOAD,

        /// <summary>
        /// Game is in the team showcase state.
        /// </summary>
        DOTA_GAMERULES_STATE_TEAM_SHOWCASE
    }

    /// <summary>
    /// Enum list for each player team.
    /// </summary>
    public enum PlayerTeam
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// No team.
        /// </summary>
        None = 0,

        /// <summary>
        /// Spectator.
        /// </summary>
        Spectator = 1,

        /// <summary>
        /// Radiant team.
        /// </summary>
        Radiant = 2,

        /// <summary>
        /// Dire team.
        /// </summary>
        Dire = 3
    }

    /// <summary>
    /// Enum list for each Roshan state.
    /// </summary>
    public enum RoshanState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Alive.
        /// </summary>
        Alive,

        /// <summary>
        /// Waiting for respawn at base respawn rate.
        /// </summary>
        Respawn_Base,

        /// <summary>
        /// Waiting for respawn at varied respawn rate.
        /// </summary>
        Respawn_Variable
    }

    /// <summary>
    /// Class representing information about the map.
    /// </summary>
    public class Map : Node
    {
        /// <summary>
        /// Name of the current map.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Match ID of the current game.
        /// </summary>
        public readonly long MatchID;

        /// <summary>
        /// Game time.
        /// </summary>
        public readonly int GameTime;

        /// <summary>
        /// Clock time (time shown at the top of the game hud).
        /// </summary>
        public readonly int ClockTime;

        /// <summary>
        /// A boolean representing whether it is daytime.
        /// </summary>
        public readonly bool IsDaytime;

        /// <summary>
        /// A boolean representing whether Nightstalker forced night time.
        /// </summary>
        public readonly bool IsNightstalker_Night;

        /// <summary>
        /// Current Radiant team score.
        /// </summary>
        public readonly int RadiantScore;

        /// <summary>
        /// Current Dire team score.
        /// </summary>
        public readonly int DireScore;

        /// <summary>
        /// Current game state.
        /// </summary>
        public readonly DOTA_GameState GameState;

        /// <summary>
        /// A boolean representing whether the game is currently paused.
        /// </summary>
        public readonly bool IsPaused;

        /// <summary>
        /// The winning team.
        /// </summary>
        public readonly PlayerTeam WinningTeam;

        /// <summary>
        /// The name of the custom game.
        /// </summary>
        public readonly string CustomGameName;

        /// <summary>
        /// The cooldown on ward purchases.
        /// </summary>
        public readonly int WardPurchaseCooldown;

        /// <summary>
        /// The cooldown on ward purchases for the Radiant team. (SPECTATOR ONLY)
        /// </summary>
        public readonly int RadiantWardPurchaseCooldown;

        /// <summary>
        /// The cooldown on ward purchases for the Dire team. (SPECTATOR ONLY)
        /// </summary>
        public readonly int DireWardPurchaseCooldown;

        /// <summary>
        /// The state of Roshan. (SPECTATOR ONLY)
        /// </summary>
        public readonly RoshanState RoshanState;

        /// <summary>
        /// The time in seconds until the Roshan state changes. (SPECTATOR ONLY)
        /// </summary>
        public readonly int RoshanStateEndTime;

        internal Map(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            MatchID = GetLong("matchid");
            GameTime = GetInt("game_time");
            ClockTime = GetInt("clock_time");
            IsDaytime = GetBool("daytime");
            IsNightstalker_Night = GetBool("nightstalker_night");
            RadiantScore = GetInt("radiant_score");
            DireScore = GetInt("dire_score");
            GameState = GetEnum<DOTA_GameState>("game_state");
            IsPaused = GetBool("paused");
            WinningTeam = GetEnum<PlayerTeam>("win_team");
            CustomGameName = GetString("customgamename");
            RadiantWardPurchaseCooldown = GetInt("radiant_ward_purchase_cooldown");
            DireWardPurchaseCooldown = GetInt("dire_ward_purchase_cooldown");
            // There is mentions of "radiant_win_chance" in game code, but no mention of "dire_win_chance".
            // Omitting "radiant_win_chance" since there is no dire counterpart.
            RoshanState = GetEnum<RoshanState>("roshan_state");
            RoshanStateEndTime = GetInt("roshan_state_end_seconds");
            WardPurchaseCooldown = GetInt("ward_purchase_cooldown");
        }
    }
}
