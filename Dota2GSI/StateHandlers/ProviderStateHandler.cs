using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class ProviderStateHandler : EventHandler<DotaGameEvent>
    {
        public ProviderStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<ProviderUpdated>(OnProviderStateUpdated);
        }

        ~ProviderStateHandler()
        {
            dispatcher.Unsubscribe<ProviderUpdated>(OnProviderStateUpdated);
        }

        private void OnProviderStateUpdated(DotaGameEvent e)
        {
            ProviderUpdated evt = (e as ProviderUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
