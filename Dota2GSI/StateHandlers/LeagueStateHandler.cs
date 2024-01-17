using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class LeagueStateHandler : EventHandler<DotaGameEvent>
    {
        public LeagueStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<LeagueUpdated>(OnLeagueStateUpdated);
        }

        ~LeagueStateHandler()
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
