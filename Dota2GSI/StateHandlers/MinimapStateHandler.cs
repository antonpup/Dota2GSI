using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class MinimapStateHandler : EventHandler<DotaGameEvent>
    {
        public MinimapStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<MinimapUpdated>(OnMinimapStateUpdated);
        }

        ~MinimapStateHandler()
        {
            dispatcher.Unsubscribe<MinimapUpdated>(OnMinimapStateUpdated);
        }

        private void OnMinimapStateUpdated(DotaGameEvent e)
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
        }
    }
}
