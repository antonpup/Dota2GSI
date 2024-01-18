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

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new PlayerWearablesUpdated(evt.New.LocalPlayer, evt.Previous.LocalPlayer, _player_cache[-1]));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new PlayerWearablesUpdated(player_kvp.Value, previous_hero_details, _player_cache[player_kvp.Key]));
                    }
                }
            }
        }
    }
}
