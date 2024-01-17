﻿using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class NeutralItemsStateHandler : EventHandler<DotaGameEvent>
    {
        public NeutralItemsStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<NeutralItemsUpdated>(OnNeutralItemsStateUpdated);
        }

        ~NeutralItemsStateHandler()
        {
            dispatcher.Unsubscribe<NeutralItemsUpdated>(OnNeutralItemsStateUpdated);
        }

        private void OnNeutralItemsStateUpdated(DotaGameEvent e)
        {
            NeutralItemsUpdated evt = (e as NeutralItemsUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var neutral_items_kvp in evt.New.TeamItems)
            {
                if (!evt.Previous.TeamItems.ContainsKey(neutral_items_kvp.Key))
                {
                    continue;
                }

                var previous_neutral_items = evt.Previous.TeamItems[neutral_items_kvp.Key];

                if (!neutral_items_kvp.Value.Equals(previous_neutral_items))
                {
                    dispatcher.Broadcast(new TeamNeutralItemsUpdated(neutral_items_kvp.Value, previous_neutral_items, neutral_items_kvp.Key));
                }
            }
        }
    }
}
