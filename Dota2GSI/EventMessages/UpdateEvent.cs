using Dota2GSI.Nodes;
using Dota2GSI.Nodes.Helpers;

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

        public ValueEvent(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Event for specific player's single value update.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated player details.
        /// </summary>
        public readonly FullPlayerDetails Player;

        public PlayerValueEvent(T value, FullPlayerDetails player) : base(value)
        {
            Player = player;
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

        public TeamValueEvent(T value, PlayerTeam team) : base(value)
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

        public UpdateEvent(T new_value, T previous_value)
        {
            New = new_value;
            Previous = previous_value;
        }
    }

    /// <summary>
    /// Event for specific player's value change.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class PlayerUpdateEvent<T> : UpdateEvent<T>
    {
        /// <summary>
        /// The associated player.
        /// </summary>
        public readonly FullPlayerDetails Player;

        public PlayerUpdateEvent(T new_value, T previous_value, FullPlayerDetails player) : base(new_value, previous_value)
        {
            Player = player;
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

        public TeamUpdateEvent(T new_value, T previous_value, PlayerTeam team) : base(new_value, previous_value)
        {
            Team = team;
        }
    }

    /// <summary>
    /// Event for specific entity's single value update.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityValueEvent<T> : ValueEvent<T>
    {
        /// <summary>
        /// The associated entity ID.
        /// </summary>
        public readonly string EntityID;

        public EntityValueEvent(T value, string entity_id) : base(value)
        {
            EntityID = entity_id;
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

        public EntityUpdateEvent(T new_value, T previous_value, string entity_id) : base(new_value, previous_value)
        {
            EntityID = entity_id;
        }
    }
}
