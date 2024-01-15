using Dota2GSI.Nodes;
using Dota2GSI.Nodes.ItemsProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Items update. 
    /// </summary>
    public class ItemsStateUpdated : UpdateEvent<Items>
    {
        public ItemsStateUpdated(Items new_value, Items previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Item Details update.
    /// </summary>
    public class ItemDetailsChanged : PlayerUpdateEvent<ItemDetails>
    {
        public ItemDetailsChanged(ItemDetails new_value, ItemDetails previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }
}
