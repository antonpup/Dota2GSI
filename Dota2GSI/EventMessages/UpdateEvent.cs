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
        /// The associated player details.
        /// </summary>
        public readonly FullPlayerDetails Player;

        public PlayerValueEvent(T obj, FullPlayerDetails player) : base(obj)
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
        /// The associated player.
        /// </summary>
        public readonly FullPlayerDetails Player;

        public PlayerUpdateEvent(T new_obj, T previous_obj, FullPlayerDetails player) : base(new_obj, previous_obj)
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
