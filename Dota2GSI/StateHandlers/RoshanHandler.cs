using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class RoshanHandler : EventHandler<DotaGameEvent>
    {
        public RoshanHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<RoshanUpdated>(OnRoshanStateUpdated);
        }

        ~RoshanHandler()
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
