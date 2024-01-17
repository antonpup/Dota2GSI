using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class RoshanStateHandler : EventHandler<DotaGameEvent>
    {
        public RoshanStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<RoshanUpdated>(OnRoshanStateUpdated);
        }

        ~RoshanStateHandler()
        {
            dispatcher.Unsubscribe<RoshanUpdated>(OnRoshanStateUpdated);
        }

        private void OnRoshanStateUpdated(DotaGameEvent e)
        {
            RoshanUpdated evt = (e as RoshanUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
