using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;
using Dota2GSI.Nodes.Helpers;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Events update. 
    /// </summary>
    public class EventsUpdated : UpdateEvent<Events>
    {
        public EventsUpdated(Events new_value, Events previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for Gameplay Event.
    /// </summary>
    public class GameplayEvent : ValueEvent<Event>
    {
        public GameplayEvent(Event value) : base(value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Gameplay Event.
    /// </summary>
    public class TeamGameplayEvent : TeamValueEvent<Event>
    {
        public TeamGameplayEvent(Event value, PlayerTeam team) : base(value, team)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Event.
    /// </summary>
    public class PlayerGameplayEvent : PlayerValueEvent<Event>
    {
        public PlayerGameplayEvent(Event value, FullPlayerDetails player) : base(value, player)
        {
        }
    }
}
