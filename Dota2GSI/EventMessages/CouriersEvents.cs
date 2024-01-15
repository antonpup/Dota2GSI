using Dota2GSI.Nodes;
using Dota2GSI.Nodes.CouriersProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Couriers update. 
    /// </summary>
    public class CouriersStateUpdated : UpdateEvent<Couriers>
    {
        public CouriersStateUpdated(Couriers new_value, Couriers previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific player's Courier update.
    /// </summary>
    public class CourierUpdated : PlayerUpdateEvent<Courier>
    {
        public CourierUpdated(Courier new_value, Courier previous_value, int player_id) : base(new_value, previous_value, player_id)
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
