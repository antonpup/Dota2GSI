using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;
using Dota2GSI.Nodes.EventsProvider;
using System.Linq;

namespace Dota2GSI
{
    public class EventsStateHandler : EventHandler<DotaGameEvent>
    {
        private Player current_player_state = new Player();

        public EventsStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<EventsUpdated>(OnEventsStateUpdated);
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerStateUpdated);
        }

        ~EventsStateHandler()
        {
            dispatcher.Unsubscribe<EventsUpdated>(OnEventsStateUpdated);
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerStateUpdated);
        }

        private void OnPlayerStateUpdated(DotaGameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            current_player_state = evt.New;
        }

        private void OnEventsStateUpdated(DotaGameEvent e)
        {
            EventsUpdated evt = (e as EventsUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var game_event in evt.New)
            {
                Event found_event = evt.Previous.FirstOrDefault((Event element) => { return element.Equals(game_event); }, null);

                if (found_event == null)
                {
                    dispatcher.Broadcast(new GameplayEvent(game_event));

                    switch (game_event.EventType)
                    {
                        case EventType.Courier_killed:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.KillerPlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, game_event.Team));
                            break;
                        case EventType.Roshan_killed:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.KillerPlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, game_event.Team));
                            break;
                        case EventType.Aegis_picked_up:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.PlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, current_player_state.GetForPlayer(game_event.PlayerID).Team));
                            break;
                        case EventType.Aegis_denied:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.PlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, current_player_state.GetForPlayer(game_event.PlayerID).Team));
                            break;
                        case EventType.Tip:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.PlayerID));
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.TipReceiverPlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, current_player_state.GetForPlayer(game_event.TipReceiverPlayerID).Team));
                            break;
                        case EventType.Bounty_rune_pickup:
                            dispatcher.Broadcast(new PlayerEvent(game_event, game_event.PlayerID));
                            dispatcher.Broadcast(new TeamEvent(game_event, game_event.Team));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
