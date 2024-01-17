using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;

namespace Dota2GSI
{
    public class CouriersStateHandler : EventHandler<DotaGameEvent>
    {
        private Player current_player_state = new Player();

        public CouriersStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<CouriersUpdated>(OnCouriersStateUpdated);
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerStateUpdated);
        }

        ~CouriersStateHandler()
        {
            dispatcher.Unsubscribe<CouriersUpdated>(OnCouriersStateUpdated);
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerStateUpdated);
        }

        private void OnPlayerStateUpdated(DotaGameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            current_player_state = evt.New;
        }

        private void OnCouriersStateUpdated(DotaGameEvent e)
        {
            CouriersUpdated evt = (e as CouriersUpdated);

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
