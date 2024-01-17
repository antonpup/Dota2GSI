using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class CouriersHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public CouriersHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<CouriersUpdated>(OnCouriersStateUpdated);
        }

        ~CouriersHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<CouriersUpdated>(OnCouriersStateUpdated);
        }

        private void OnFullPlayerDetailsUpdated(DotaGameEvent e)
        {
            FullPlayerDetailsUpdated evt = (e as FullPlayerDetailsUpdated);

            if (evt == null)
            {
                return;
            }

            _player_cache[evt.New.PlayerID] = evt.New;
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
                    var player_details = _player_cache[courier_kvp.Value.OwnerID];
                    dispatcher.Broadcast(new CourierUpdated(courier_kvp.Value, previous_courier, player_details));

                    dispatcher.Broadcast(new TeamCourierUpdated(courier_kvp.Value, previous_courier, player_details.Details.Team));
                }
            }
        }
    }
}
