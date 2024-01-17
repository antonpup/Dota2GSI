using Dota2GSI.Nodes;
using Dota2GSI.Nodes.CouriersProvider;
using Dota2GSI.Nodes.Helpers;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Couriers update. 
    /// </summary>
    public class CouriersUpdated : UpdateEvent<Couriers>
    {
        public CouriersUpdated(Couriers new_value, Couriers previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Courier update.
    /// </summary>
    public class CourierUpdated : PlayerUpdateEvent<Courier>
    {
        public CourierUpdated(Courier new_value, Courier previous_value, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Courier update.
    /// </summary>
    public class TeamCourierUpdated : TeamUpdateEvent<Courier>
    {
        public TeamCourierUpdated(Courier new_value, Courier previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }
}
