using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class ItemsHandler : EventHandler<DotaGameEvent>
    {
        public ItemsHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<ItemsUpdated>(OnItemsStateUpdated);
        }

        ~ItemsHandler()
        {
            dispatcher.Unsubscribe<ItemsUpdated>(OnItemsStateUpdated);
        }

        private void OnItemsStateUpdated(DotaGameEvent e)
        {
            ItemsUpdated evt = (e as ItemsUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new ItemDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    // Get corresponding previous hero details.
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new ItemDetailsChanged(player_kvp.Value, previous_hero_details, player_kvp.Key));
                    }
                }
            }
        }
    }
}
