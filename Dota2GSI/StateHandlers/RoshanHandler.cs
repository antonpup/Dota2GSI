using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class RoshanHandler : EventHandler<DotaGameEvent>
    {
        public RoshanHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<RoshanUpdated>(OnRoshanUpdated);
        }

        ~RoshanHandler()
        {
            dispatcher.Unsubscribe<RoshanUpdated>(OnRoshanUpdated);
        }

        private void OnRoshanUpdated(DotaGameEvent e)
        {
            RoshanUpdated evt = (e as RoshanUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
