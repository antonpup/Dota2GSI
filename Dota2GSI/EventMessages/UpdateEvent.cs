using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for a single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class ValueEvent<T> : DotaGameEvent
    {
        /// <summary>
        /// Value.
        /// </summary>
        public readonly T Value;

        public ValueEvent(T obj)
        {
            Value = obj;
        }
    }

    /// <summary>
    /// Event for specific player's single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated player ID.
        /// When local player, the player ID is -1.
        /// </summary>
        public readonly int PlayerID;

        public PlayerValueEvent(T obj, int player_id = -1) : base(obj)
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for specific team's single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class TeamValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamValueEvent(T obj, PlayerTeam team) : base(obj)
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for value change.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class UpdateEvent<T> : DotaGameEvent
    {
        /// <summary>
        /// New value.
        /// </summary>
        public readonly T New;

        /// <summary>
        /// Previous value.
        /// </summary>
        public readonly T Previous;

        public UpdateEvent(T new_obj, T previous_obj)
        {
            New = new_obj;
            Previous = previous_obj;
        }
    }

    /// <summary>
    /// Event for specific player's value change.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated player ID.
        /// When local player, the player ID is -1.
        /// </summary>
        public readonly int PlayerID;

        public PlayerUpdateEvent(T new_obj, T previous_obj, int player_id = -1) : base(new_obj, previous_obj)
        {
            PlayerID = player_id;
        }
    }

    /// <summary>
    /// Event for specific team's value change.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TeamUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated team.
        /// </summary>
        public readonly PlayerTeam Team;

        public TeamUpdateEvent(T new_obj, T previous_obj, PlayerTeam team) : base(new_obj, previous_obj)
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for specific entity's value change.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated entity ID.
        /// </summary>
        public readonly string EntityID;

        public EntityUpdateEvent(T new_obj, T previous_obj, string entity_id) : base(new_obj, previous_obj)
        {
            EntityID = entity_id;
        }
    }
}
