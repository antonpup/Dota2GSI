using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class WearablesHandler : EventHandler<DotaGameEvent>
    {
        public WearablesHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<WearablesUpdated>(OnWearablesStateUpdated);
        }

        ~WearablesHandler()
        {
            dispatcher.Unsubscribe<WearablesUpdated>(OnWearablesStateUpdated);
        }

        private void OnWearablesStateUpdated(DotaGameEvent e)
        {
            WearablesUpdated evt = (e as WearablesUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new PlayerWearablesUpdated(evt.New.LocalPlayer, evt.Previous.LocalPlayer));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new PlayerWearablesUpdated(player_kvp.Value, previous_hero_details, player_kvp.Key));
                    }
                }
            }
        }
    }
}
