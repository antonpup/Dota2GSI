using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class RoshanStateHandler : EventHandler<DotaGameEvent>
    {
        public RoshanStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<RoshanStateUpdated>(OnRoshanStateUpdated);
        }

        ~RoshanStateHandler()
        {
            dispatcher.Unsubscribe<RoshanStateUpdated>(OnRoshanStateUpdated);
        }

        private void OnRoshanStateUpdated(DotaGameEvent e)
        {
            RoshanStateUpdated roshan_state_updated_event = (e as RoshanStateUpdated);

            if (roshan_state_updated_event == null)
            {
                return;
            }
        }
    }
}
