using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class MinimapHandler : EventHandler<DotaGameEvent>
    {
        public MinimapHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<MinimapUpdated>(OnMinimapUpdated);
        }

        ~MinimapHandler()
        {
            dispatcher.Unsubscribe<MinimapUpdated>(OnMinimapUpdated);
        }

        private void OnMinimapUpdated(DotaGameEvent e)
        {
            MinimapUpdated evt = (e as MinimapUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var element_kvp in evt.New.Elements)
            {
                if (!evt.Previous.Elements.ContainsKey(element_kvp.Key))
                {
                    // The element did not exist before.
                    dispatcher.Broadcast(new MinimapElementAdded(element_kvp.Value, element_kvp.Key.ToString()));
                    continue;
                }

                var previous_element = evt.Previous.Elements[element_kvp.Key];

                if (!element_kvp.Value.Equals(previous_element))
                {
                    dispatcher.Broadcast(new MinimapElementUpdated(element_kvp.Value, previous_element, element_kvp.Key.ToString()));

                    if (element_kvp.Value.Team != Nodes.PlayerTeam.Undefined && element_kvp.Value.Team != Nodes.PlayerTeam.Unknown)
                    {
                        dispatcher.Broadcast(new TeamMinimapElementUpdated(element_kvp.Value, previous_element, element_kvp.Value.Team));
                    }
                }
            }

            foreach (var element_kvp in evt.Previous.Elements)
            {
                if (!evt.New.Elements.ContainsKey(element_kvp.Key))
                {
                    // The element does not exist anymore.
                    dispatcher.Broadcast(new MinimapElementRemoved(element_kvp.Value, element_kvp.Key.ToString()));
                }
            }
        }
    }
}
