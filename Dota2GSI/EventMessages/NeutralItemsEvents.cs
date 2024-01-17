using Dota2GSI.Nodes;
using Dota2GSI.Nodes.NeutralItemsProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Neutral Items update. 
    /// </summary>
    public class NeutralItemsUpdated : UpdateEvent<NeutralItems>
    {
        public NeutralItemsUpdated(NeutralItems new_value, NeutralItems previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Neutral Items update. 
    /// </summary>
    public class TeamNeutralItemsUpdated : TeamUpdateEvent<TeamNeutralItems>
    {
        public TeamNeutralItemsUpdated(TeamNeutralItems new_value, TeamNeutralItems previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }
}
