using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class LeagueHandler : EventHandler<DotaGameEvent>
    {
        public LeagueHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<LeagueUpdated>(OnLeagueStateUpdated);
        }

        ~LeagueHandler()
        {
            dispatcher.Unsubscribe<LeagueUpdated>(OnLeagueStateUpdated);
        }

        private void OnLeagueStateUpdated(DotaGameEvent e)
        {
            LeagueUpdated evt = (e as LeagueUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
