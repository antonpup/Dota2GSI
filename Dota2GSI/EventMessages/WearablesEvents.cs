using Dota2GSI.Nodes;
using Dota2GSI.Nodes.Helpers;
using Dota2GSI.Nodes.WearablesProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Wearables update. 
    /// </summary>
    public class WearablesUpdated : UpdateEvent<Wearables>
    {
        public WearablesUpdated(Wearables new_value, Wearables previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Wearables update.
    /// </summary>
    public class PlayerWearablesUpdated : PlayerUpdateEvent<PlayerWearables>
    {
        public PlayerWearablesUpdated(PlayerWearables new_value, PlayerWearables previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }
}
