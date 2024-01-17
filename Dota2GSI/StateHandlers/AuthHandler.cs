using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class AuthHandler : EventHandler<DotaGameEvent>
    {
        public AuthHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AuthUpdated>(OnAuthStateUpdated);
        }

        ~AuthHandler()
        {
            dispatcher.Unsubscribe<AuthUpdated>(OnAuthStateUpdated);
        }

        private void OnAuthStateUpdated(DotaGameEvent e)
        {
            AuthUpdated evt = (e as AuthUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
