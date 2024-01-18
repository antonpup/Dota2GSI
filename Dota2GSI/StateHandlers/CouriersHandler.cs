using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class CouriersHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public CouriersHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<CouriersUpdated>(OnCouriersUpdated);
            dispatcher.Subscribe<CourierUpdated>(OnCourierUpdated);
        }

        ~CouriersHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<CouriersUpdated>(OnCouriersUpdated);
            dispatcher.Unsubscribe<CourierUpdated>(OnCourierUpdated);
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

        private void OnCouriersUpdated(DotaGameEvent e)
        {
            CouriersUpdated evt = (e as CouriersUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var courier_kvp in evt.New.CouriersMap)
            {
                var previous_courier = evt.Previous.GetForPlayer(courier_kvp.Value.OwnerID);
                if (!courier_kvp.Value.Equals(previous_courier))
                {
                    var player_details = _player_cache[courier_kvp.Value.OwnerID];
                    dispatcher.Broadcast(new CourierUpdated(courier_kvp.Value, previous_courier, player_details));

                    dispatcher.Broadcast(new TeamCourierUpdated(courier_kvp.Value, previous_courier, player_details.Details.Team));
                }
            }
        }

        private void OnCourierUpdated(DotaGameEvent e)
        {
            CourierUpdated evt = (e as CourierUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var item in evt.New.Items)
            {
                if (!evt.Previous.InventoryContains(item.Value.Name))
                {
                    // Courier gained an item.
                    dispatcher.Broadcast(new CourierItemAdded(item.Value, evt.New, evt.Player));
                    continue;
                }

                var previous_item = evt.Previous.GetInventoryItem(item.Value.Name);

                if (!item.Value.Equals(previous_item))
                {
                    // Item updated.
                    dispatcher.Broadcast(new CourierItemUpdated(item.Value, previous_item, evt.New, evt.Player));
                }
            }

            foreach (var item in evt.Previous.Items)
            {
                if (!evt.New.InventoryContains(item.Value.Name))
                {
                    // Player lost an inventory item.
                    dispatcher.Broadcast(new CourierItemRemoved(item.Value, evt.New, evt.Player));
                }
            }
        }
    }
}
