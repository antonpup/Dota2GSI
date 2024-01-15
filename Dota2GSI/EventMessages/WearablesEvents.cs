using Dota2GSI.Nodes;
using Dota2GSI.Nodes.WearablesProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Wearables update. 
    /// </summary>
    public class WearablesStateUpdated : UpdateEvent<Wearables>
    {
        public WearablesStateUpdated(Wearables new_value, Wearables previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Wearables update.
    /// </summary>
    public class PlayerWearablesUpdated : PlayerUpdateEvent<PlayerWearables>
    {
        public PlayerWearablesUpdated(PlayerWearables new_value, PlayerWearables previous_value, int player_id = -1) : base(new_value, previous_value, player_id)
        {
        }
    }
}
