using Dota2GSI.Nodes;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall League update. 
    /// </summary>
    public class LeagueUpdated : UpdateEvent<League>
    {
        public LeagueUpdated(League new_value, League previous_value) : base(new_value, previous_value)
        {
        }
    }
}
