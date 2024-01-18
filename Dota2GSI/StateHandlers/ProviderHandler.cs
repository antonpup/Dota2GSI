using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class ProviderHandler : EventHandler<DotaGameEvent>
    {
        public ProviderHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<ProviderUpdated>(OnProviderUpdated);
        }

        ~ProviderHandler()
        {
            dispatcher.Unsubscribe<ProviderUpdated>(OnProviderUpdated);
        }

        private void OnProviderUpdated(DotaGameEvent e)
        {
            ProviderUpdated evt = (e as ProviderUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
