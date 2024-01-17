using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class FullDetailsHandler : EventHandler<DotaGameEvent>
    {
        public FullDetailsHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<FullTeamDetailsUpdated>(OnFullTeamDetailsUpdated);
        }

        ~FullDetailsHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<FullTeamDetailsUpdated>(OnFullTeamDetailsUpdated);
        }

        private void OnFullPlayerDetailsUpdated(DotaGameEvent e)
        {
            FullPlayerDetailsUpdated evt = (e as FullPlayerDetailsUpdated);

            if (evt == null)
            {
                return;
            }
        }

        private void OnFullTeamDetailsUpdated(DotaGameEvent e)
        {
            FullTeamDetailsUpdated evt = (e as FullTeamDetailsUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var player_kvp in evt.New.Players)
            {
                if (!evt.Previous.Players.ContainsKey(player_kvp.Key))
                {
                    // The player did not exist before.
                    dispatcher.Broadcast(new PlayerConnected(player_kvp.Value));
                    continue;
                }

                var previous_player = evt.Previous.Players[player_kvp.Key];

                if (!player_kvp.Value.Equals(previous_player))
                {
                    dispatcher.Broadcast(new FullPlayerDetailsUpdated(player_kvp.Value, previous_player));
                }
            }

            foreach (var player_kvp in evt.Previous.Players)
            {
                if (!evt.New.Players.ContainsKey(player_kvp.Key))
                {
                    // The player does not exist anymore.
                    dispatcher.Broadcast(new PlayerDisconnected(player_kvp.Value));
                }
            }
        }
    }
}
