using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class ProviderStateHandler : EventHandler<DotaGameEvent>
    {
        public ProviderStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<ProviderStateUpdated>(OnProviderStateUpdated);
        }

        ~ProviderStateHandler()
        {
            dispatcher.Unsubscribe<ProviderStateUpdated>(OnProviderStateUpdated);
        }

        private void OnProviderStateUpdated(DotaGameEvent e)
        {
            ProviderStateUpdated evt = (e as ProviderStateUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
