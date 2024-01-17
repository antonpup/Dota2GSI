using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class AuthStateHandler : EventHandler<DotaGameEvent>
    {
        public AuthStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AuthUpdated>(OnAuthStateUpdated);
        }

        ~AuthStateHandler()
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
