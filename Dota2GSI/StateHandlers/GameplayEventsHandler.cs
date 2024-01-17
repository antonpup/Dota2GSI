using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.EventsProvider;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Dota2GSI
{
    public class GameplayEventsHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public GameplayEventsHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<EventsUpdated>(OnEventsStateUpdated);
        }

        ~GameplayEventsHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<EventsUpdated>(OnEventsStateUpdated);
        }

        private void OnFullPlayerDetailsUpdated(DotaGameEvent e)
        {
            FullPlayerDetailsUpdated evt = (e as FullPlayerDetailsUpdated);

            if (evt == null)
            {
                return;
            }

            _player_cache[evt.New.PlayerID] = evt.New;
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
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.KillerPlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            break;
                        case EventType.Roshan_killed:
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.KillerPlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            break;
                        case EventType.Aegis_picked_up:
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.PlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, _player_cache[game_event.PlayerID].Details.Team));
                            break;
                        case EventType.Aegis_denied:
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.PlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, _player_cache[game_event.PlayerID].Details.Team));
                            break;
                        case EventType.Tip:
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.PlayerID]));
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.TipReceiverPlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, _player_cache[game_event.TipReceiverPlayerID].Details.Team));
                            break;
                        case EventType.Bounty_rune_pickup:
                            dispatcher.Broadcast(new PlayerGameplayEvent(game_event, _player_cache[game_event.PlayerID]));
                            dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
