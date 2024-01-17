using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.AbilitiesProvider;
using System.Linq;

namespace Dota2GSI
{
    public class AbilitiesStateHandler : EventHandler<DotaGameEvent>
    {
        public AbilitiesStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<AbilitiesUpdated>(OnAbilitiesStateUpdated);
            dispatcher.Subscribe<AbilityDetailsChanged>(OnAbilityDetailsChanged);
        }

        ~AbilitiesStateHandler()
        {
            dispatcher.Unsubscribe<AbilitiesUpdated>(OnAbilitiesStateUpdated);
            dispatcher.Unsubscribe<AbilityDetailsChanged>(OnAbilityDetailsChanged);
        }

        private void OnAbilitiesStateUpdated(DotaGameEvent e)
        {
            AbilitiesUpdated evt = (e as AbilitiesUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new AbilityDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    // Get corresponding previous hero details.
                    var previous_ability_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_ability_details))
                    {
                        dispatcher.Broadcast(new AbilityDetailsChanged(player_kvp.Value, previous_ability_details, player_kvp.Key));
                    }
                }
            }
        }

        private void OnAbilityDetailsChanged(DotaGameEvent e)
        {
            AbilityDetailsChanged evt = (e as AbilityDetailsChanged);

            if (evt == null)
            {
                return;
            }

            foreach (var ability in evt.New)
            {
                Ability found_ability = evt.Previous.FirstOrDefault((Ability element) => { return element.Name.Equals(ability.Name); }, null);

                if (found_ability == null)
                {
                    dispatcher.Broadcast(new AbilityAdded(ability, evt.PlayerID));
                }

                if (!ability.Equals(found_ability))
                {
                    dispatcher.Broadcast(new AbilityUpdated(ability, found_ability, evt.PlayerID));
                }
            }

            foreach (var ability in evt.Previous)
            {
                Ability found_ability = evt.New.FirstOrDefault((Ability element) => { return element.Name.Equals(ability.Name); }, null);

                if (found_ability == null)
                {
                    dispatcher.Broadcast(new AbilityRemoved(ability, evt.PlayerID));
                }
            }
        }
    }
}
