using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Map update. 
    /// </summary>
    public class MapUpdated : UpdateEvent<Map>
    {
        public MapUpdated(Map new_value, Map previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Time of Day change.
    /// </summary>
    public class TimeOfDayChanged : DotaGameEvent
    {
        /// <summary>
        /// True when day time, false when night time.
        /// </summary>
        public readonly bool IsDaytime;

        /// <summary>
        /// True when nightstalker night, false otherwise.
        /// </summary>
        public readonly bool IsNightstalkerNight;

        public TimeOfDayChanged(bool is_day, bool is_nightstalker_night) : base()
        {
            IsDaytime = is_day;
            IsNightstalkerNight = is_nightstalker_night;
        }
    }

    /// <summary>
    /// Event for specific team's score change.
    /// </summary>
    public class TeamScoreChanged : TeamUpdateEvent<int>
    {
        public TeamScoreChanged(int new_value, int previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }

    /// <summary>
    /// Event for Game State change.
    /// </summary>
    public class GameStateChanged : UpdateEvent<DOTA_GameState>
    {
        public GameStateChanged(DOTA_GameState new_value, DOTA_GameState previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Game Pause State change.
    /// </summary>
    public class PauseStateChanged : UpdateEvent<bool>
    {
        public PauseStateChanged(bool new_value, bool previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Game Paused.
    /// </summary>
    public class GamePaused : DotaGameEvent
    {
        public GamePaused() : base()
        {
        }
    }

    /// <summary>
    /// Event for Game Resumed.
    /// </summary>
    public class GameResumed : DotaGameEvent
    {
        public GameResumed() : base()
        {
        }
    }

    /// <summary>
    /// Event for specific team's Victory.
    /// </summary>
    public class TeamVictory : DotaGameEvent
    {
        /// <summary>
        /// The winning team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamVictory(PlayerTeam team) : base()
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for specific team's Defeat.
    /// </summary>
    public class TeamDefeat : DotaGameEvent
    {
        /// <summary>
        /// The losing team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamDefeat(PlayerTeam team) : base()
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for Roshan State change.
    /// </summary>
    public class RoshanStateChanged : UpdateEvent<RoshanState>
    {
        public RoshanStateChanged(RoshanState new_value, RoshanState previous_value) : base(new_value, previous_value)
        {
        }
    }
}
