using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;

namespace Dota2GSI
{
    public class CouriersStateHandler : EventHandler<DotaGameEvent>
    {
        private Player current_player_state = new Player();

        public CouriersStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<CouriersStateUpdated>(OnCouriersStateUpdated);
            dispatcher.Subscribe<PlayerStateUpdated>(OnPlayerStateUpdated);
        }

        ~CouriersStateHandler()
        {
            dispatcher.Unsubscribe<CouriersStateUpdated>(OnCouriersStateUpdated);
            dispatcher.Unsubscribe<PlayerStateUpdated>(OnPlayerStateUpdated);
        }

        private void OnPlayerStateUpdated(DotaGameEvent e)
        {
            PlayerStateUpdated evt = (e as PlayerStateUpdated);

            if (evt == null)
            {
                return;
            }

            current_player_state = evt.New;
        }

        private void OnCouriersStateUpdated(DotaGameEvent e)
        {
            CouriersStateUpdated evt = (e as CouriersStateUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var courier_kvp in evt.New.CouriersMap)
            {
                var previous_courier = evt.Previous.GetForPlayer(courier_kvp.Value.OwnerID);
                if (!courier_kvp.Value.Equals(previous_courier))
                {
                    dispatcher.Broadcast(new CourierUpdated(courier_kvp.Value, previous_courier, courier_kvp.Value.OwnerID));

                    var courier_team = current_player_state.GetForPlayer(courier_kvp.Value.OwnerID).Team;
                    dispatcher.Broadcast(new TeamCourierUpdated(courier_kvp.Value, previous_courier, courier_team));
                }
            }
        }
    }
}
