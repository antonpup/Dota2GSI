using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class WearablesHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public WearablesHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<WearablesUpdated>(OnWearablesUpdated);
        }

        ~WearablesHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<WearablesUpdated>(OnWearablesUpdated);
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

        private void OnWearablesUpdated(DotaGameEvent e)
        {
            WearablesUpdated evt = (e as WearablesUpdated);

            if (evt == null)
            {
                return;
            }

            var local_player_details = _player_cache.GetValueOrDefault(-1, null);

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer) && local_player_details != null)
            {
                dispatcher.Broadcast(new PlayerWearablesUpdated(evt.New.LocalPlayer, evt.Previous.LocalPlayer, local_player_details));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var player_details = _player_cache.GetValueOrDefault(player_kvp.Key, null);
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details) && player_details != null)
                    {
                        dispatcher.Broadcast(new PlayerWearablesUpdated(player_kvp.Value, previous_hero_details, player_details));
                    }
                }
            }
        }
    }
}
