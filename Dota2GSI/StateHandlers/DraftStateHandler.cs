using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class DraftStateHandler : EventHandler<DotaGameEvent>
    {
        public DraftStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<DraftUpdated>(OnDraftStateUpdated);
        }

        ~DraftStateHandler()
        {
            dispatcher.Unsubscribe<DraftUpdated>(OnDraftStateUpdated);
        }

        private void OnDraftStateUpdated(DotaGameEvent e)
        {
            DraftUpdated evt = (e as DraftUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                if (!evt.Previous.Teams.ContainsKey(team_kvp.Key))
                {
                    continue;
                }

                var previous_draft = evt.Previous.Teams[team_kvp.Key];

                if (!team_kvp.Value.Equals(previous_draft))
                {
                    dispatcher.Broadcast(new TeamDraftDetailsUpdated(team_kvp.Value, previous_draft, team_kvp.Key));
                }
            }
        }
    }
}
