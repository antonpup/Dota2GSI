using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class LeagueStateHandler : EventHandler<DotaGameEvent>
    {
        public LeagueStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<LeagueStateUpdated>(OnLeagueStateUpdated);
        }

        ~LeagueStateHandler()
        {
            dispatcher.Unsubscribe<LeagueStateUpdated>(OnLeagueStateUpdated);
        }

        private void OnLeagueStateUpdated(DotaGameEvent e)
        {
            LeagueStateUpdated evt = (e as LeagueStateUpdated);

            if (evt == null)
            {
                return;
            }
        }
    }
}
