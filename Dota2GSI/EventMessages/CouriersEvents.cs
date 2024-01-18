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


    /// <summary>
    /// Event for specific player's courier gaining an item in their inventory.
    /// </summary>
    public class CourierItemAdded : PlayerValueEvent<CourierItem>
    {
        /// <summary>
        /// The player's courier.
        /// </summary>
        public readonly Courier Courier;

        public CourierItemAdded(CourierItem value, Courier courier, FullPlayerDetails player) : base(value, player)
        {
            Courier = courier;
        }
    }

    /// <summary>
    /// Event for specific player's courier losing an item from their inventory.
    /// </summary>
    public class CourierItemRemoved : PlayerValueEvent<CourierItem>
    {
        /// <summary>
        /// The player's courier.
        /// </summary>
        public readonly Courier Courier;

        public CourierItemRemoved(CourierItem value, Courier courier, FullPlayerDetails player) : base(value, player)
        {
            Courier = courier;
        }
    }

    /// <summary>
    /// Event for specific player's courier item updating in their inventory.
    /// </summary>
    public class CourierItemUpdated : PlayerUpdateEvent<CourierItem>
    {
        /// <summary>
        /// The player's courier.
        /// </summary>
        public readonly Courier Courier;

        public CourierItemUpdated(CourierItem new_value, CourierItem previous_value, Courier courier, FullPlayerDetails player) : base(new_value, previous_value, player)
        {
            Courier = courier;
        }
    }
}
