using Dota2GSI.Nodes;
using Dota2GSI.Nodes.MinimapProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Minimap update. 
    /// </summary>
    public class MinimapStateUpdated : UpdateEvent<Minimap>
    {
        public MinimapStateUpdated(Minimap new_value, Minimap previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for a Minimap Element update. 
    /// </summary>
    public class MinimapElementUpdated : EntityUpdateEvent<MinimapElement>
    {
        public MinimapElementUpdated(MinimapElement new_value, MinimapElement previous_value, string entity_id) : base(new_value, previous_value, entity_id)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Minimap Element update.
    /// </summary>
    public class TeamMinimapElementUpdated : TeamUpdateEvent<MinimapElement>
    {
        public TeamMinimapElementUpdated(MinimapElement new_value, MinimapElement previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }
}
