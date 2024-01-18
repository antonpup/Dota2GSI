using Dota2GSI.Nodes;
using Dota2GSI.Nodes.Helpers;
using Dota2GSI.Nodes.ItemsProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Items update. 
    /// </summary>
    public class ItemsUpdated : UpdateEvent<Items>
    {
        public ItemsUpdated(Items new_value, Items previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Item Details update.
    /// </summary>
    public class ItemDetailsChanged : PlayerUpdateEvent<ItemDetails>
    {
        public ItemDetailsChanged(ItemDetails new_value, ItemDetails previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Item update.
    /// </summary>
    public class ItemUpdated : PlayerUpdateEvent<Item>
    {
        public ItemUpdated(Item new_value, Item previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player gaining an item in their inventory.
    /// </summary>
    public class InventoryItemAdded : PlayerValueEvent<Item>
    {
        public InventoryItemAdded(Item value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player losing an item from their inventory.
    /// </summary>
    public class InventoryItemRemoved : PlayerValueEvent<Item>
    {
        public InventoryItemRemoved(Item value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's inventory item updating.
    /// </summary>
    public class InventoryItemUpdated : ItemUpdated
    {
        public InventoryItemUpdated(Item new_value, Item previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player gaining an item in their stash.
    /// </summary>
    public class StashItemAdded : PlayerValueEvent<Item>
    {
        public StashItemAdded(Item value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player losing an item from their stash.
    /// </summary>
    public class StashItemRemoved : PlayerValueEvent<Item>
    {
        public StashItemRemoved(Item value, FullPlayerDetails player) : base(value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific player's stash item updating.
    /// </summary>
    public class StashItemUpdated : ItemUpdated
    {
        public StashItemUpdated(Item new_value, Item previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }
}
