using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class AuthStateHandler : EventHandler<DotaGameEvent>
    {
        public AuthStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AuthStateUpdated>(OnAuthStateUpdated);
        }

        ~AuthStateHandler()
        {
            dispatcher.Unsubscribe<AuthStateUpdated>(OnAuthStateUpdated);
        }

        private void OnAuthStateUpdated(DotaGameEvent e)
        {
            AuthStateUpdated evt = (e as AuthStateUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
