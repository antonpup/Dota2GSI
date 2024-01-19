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
            dispatcher.Subscribe<EventsUpdated>(OnEventsUpdated);
        }

        ~GameplayEventsHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<EventsUpdated>(OnEventsUpdated);
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

        private void OnEventsUpdated(DotaGameEvent e)
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
                            {
                                var killer_player_details = _player_cache.GetValueOrDefault(game_event.KillerPlayerID, null);
                                if (killer_player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, killer_player_details));
                                }
                                dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            }
                            break;
                        case EventType.Roshan_killed:
                            {
                                var killer_player_details = _player_cache.GetValueOrDefault(game_event.KillerPlayerID, null);
                                if (killer_player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, killer_player_details));
                                }
                                dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            }
                            break;
                        case EventType.Aegis_picked_up:
                            {
                                var player_details = _player_cache.GetValueOrDefault(game_event.PlayerID, null);
                                if (player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, player_details));
                                    dispatcher.Broadcast(new TeamGameplayEvent(game_event, player_details.Details.Team));
                                }
                            }
                            break;
                        case EventType.Aegis_denied:
                            {
                                var player_details = _player_cache.GetValueOrDefault(game_event.PlayerID, null);
                                if (player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, player_details));
                                    dispatcher.Broadcast(new TeamGameplayEvent(game_event, player_details.Details.Team));
                                }
                            }
                            break;
                        case EventType.Tip:
                            {
                                var player_details = _player_cache.GetValueOrDefault(game_event.PlayerID, null);
                                var tip_receiver_player_details = _player_cache.GetValueOrDefault(game_event.TipReceiverPlayerID, null);
                                if (player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, player_details));
                                }

                                if (tip_receiver_player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, tip_receiver_player_details));
                                    dispatcher.Broadcast(new TeamGameplayEvent(game_event, tip_receiver_player_details.Details.Team));
                                }
                            }
                            break;
                        case EventType.Bounty_rune_pickup:
                            {
                                var player_details = _player_cache.GetValueOrDefault(game_event.PlayerID, null);
                                if (player_details != null)
                                {
                                    dispatcher.Broadcast(new PlayerGameplayEvent(game_event, player_details));
                                }

                                dispatcher.Broadcast(new TeamGameplayEvent(game_event, game_event.Team));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
