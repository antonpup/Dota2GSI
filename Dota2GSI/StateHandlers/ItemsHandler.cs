using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class ItemsHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public ItemsHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<ItemsUpdated>(OnItemsUpdated);
            dispatcher.Subscribe<ItemDetailsChanged>(OnItemDetailsChanged);
        }

        ~ItemsHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<ItemsUpdated>(OnItemsUpdated);
            dispatcher.Unsubscribe<ItemDetailsChanged>(OnItemDetailsChanged);
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

        private void OnItemsUpdated(DotaGameEvent e)
        {
            ItemsUpdated evt = (e as ItemsUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new ItemDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer, _player_cache[-1]));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    // Get corresponding previous hero details.
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new ItemDetailsChanged(player_kvp.Value, previous_hero_details, _player_cache[player_kvp.Key]));
                    }
                }
            }
        }

        private void OnItemDetailsChanged(DotaGameEvent e)
        {
            ItemDetailsChanged evt = (e as ItemDetailsChanged);

            if (evt == null)
            {
                return;
            }

            foreach (var item in evt.New.Inventory)
            {
                if (!evt.Previous.InventoryContains(item.Name))
                {
                    // Player gained an inventory item.
                    dispatcher.Broadcast(new InventoryItemAdded(item, evt.Player));
                    continue;
                }

                var previous_item = evt.Previous.GetInventoryItem(item.Name);

                if (!item.Equals(previous_item))
                {
                    // Item updated.
                    dispatcher.Broadcast(new InventoryItemUpdated(item, previous_item, evt.Player));
                }
            }

            foreach (var item in evt.Previous.Inventory)
            {
                if (!evt.New.InventoryContains(item.Name))
                {
                    // Player lost an inventory item.
                    dispatcher.Broadcast(new InventoryItemRemoved(item, evt.Player));
                }
            }

            foreach (var item in evt.New.Stash)
            {
                if (!evt.Previous.StashContains(item.Name))
                {
                    // Player gained a stash item.
                    dispatcher.Broadcast(new StashItemAdded(item, evt.Player));
                    continue;
                }

                var previous_item = evt.Previous.GetStashItem(item.Name);

                if (!item.Equals(previous_item))
                {
                    // Stash item updated.
                    dispatcher.Broadcast(new StashItemUpdated(item, previous_item, evt.Player));
                }
            }

            foreach (var item in evt.Previous.Inventory)
            {
                if (!evt.New.StashContains(item.Name))
                {
                    // Player lost a stash item.
                    dispatcher.Broadcast(new StashItemRemoved(item, evt.Player));
                }
            }

            if (!evt.New.Teleport.Equals(evt.Previous.Teleport))
            {
                // Teleport item updated.
                dispatcher.Broadcast(new ItemUpdated(evt.New.Teleport, evt.Previous.Teleport, evt.Player));
            }

            if (!evt.New.Neutral.Equals(evt.Previous.Neutral))
            {
                // Neutral item updated.
                dispatcher.Broadcast(new ItemUpdated(evt.New.Neutral, evt.Previous.Neutral, evt.Player));
            }
        }
    }
}
