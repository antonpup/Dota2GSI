using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Event update. 
    /// </summary>
    public class EventsStateUpdated : UpdateEvent<Events>
    {
        public EventsStateUpdated(Events new_value, Events previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Game Event.
    /// </summary>
    public class GameEvent : ValueEvent<Event>
    {
        public GameEvent(Event value) : base(value)
        {
        }
    }

    /// <summary>
    /// Event for specific team Event.
    /// </summary>
    public class TeamEvent : TeamValueEvent<Event>
    {
        public TeamEvent(Event value, PlayerTeam team) : base(value, team)
        {
        }
    }

    /// <summary>
    /// Event for specific player Event.
    /// </summary>
    public class PlayerEvent : PlayerValueEvent<Event>
    {
        public PlayerEvent(Event value, int player_id) : base(value, player_id)
        {
        }
    }
}
