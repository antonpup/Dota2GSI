using Dota2GSI.Nodes;
using Dota2GSI.Nodes.DraftProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Draft update. 
    /// </summary>
    public class DraftUpdated : UpdateEvent<Draft>
    {
        public DraftUpdated(Draft new_value, Draft previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team Draft Details update.
    /// </summary>
    public class TeamDraftDetailsUpdated : TeamUpdateEvent<DraftDetails>
    {
        public TeamDraftDetailsUpdated(DraftDetails new_value, DraftDetails previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }
}
