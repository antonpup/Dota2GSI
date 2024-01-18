using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class AuthHandler : EventHandler<DotaGameEvent>
    {
        public AuthHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AuthUpdated>(OnAuthUpdated);
        }

        ~AuthHandler()
        {
            dispatcher.Unsubscribe<AuthUpdated>(OnAuthUpdated);
        }

        private void OnAuthUpdated(DotaGameEvent e)
        {
            AuthUpdated evt = (e as AuthUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
