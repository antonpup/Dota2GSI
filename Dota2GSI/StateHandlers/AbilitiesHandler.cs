using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.AbilitiesProvider;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Dota2GSI
{
    public class AbilitiesHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public AbilitiesHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<AbilitiesUpdated>(OnAbilitiesUpdated);
            dispatcher.Subscribe<AbilityDetailsChanged>(OnAbilityDetailsChanged);
        }

        ~AbilitiesHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<AbilitiesUpdated>(OnAbilitiesUpdated);
            dispatcher.Unsubscribe<AbilityDetailsChanged>(OnAbilityDetailsChanged);
        }

        private void OnFullPlayerDetailsUpdated(DotaGameEvent e)
        {
            FullPlayerDetailsUpdated evt = (e as FullPlayerDetailsUpdated);

            if (evt == null)
            {
                return;
            }

            _player_cache[evt.New.PlayerID] = evt.New;
        }

        private void OnAbilitiesUpdated(DotaGameEvent e)
        {
            AbilitiesUpdated evt = (e as AbilitiesUpdated);

            if (evt == null)
            {
                return;
            }

            var local_player_details = _player_cache.GetValueOrDefault(-1, null);

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer) && local_player_details != null)
            {
                dispatcher.Broadcast(new AbilityDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer, local_player_details));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    // Get corresponding previous hero details.
                    var previous_ability_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    var player_details = _player_cache.GetValueOrDefault(player_kvp.Key, null);

                    if (!player_kvp.Value.Equals(previous_ability_details) && previous_ability_details.IsValid() && player_details != null)
                    {
                        dispatcher.Broadcast(new AbilityDetailsChanged(player_kvp.Value, previous_ability_details, player_details));
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
                    dispatcher.Broadcast(new AbilityAdded(ability, evt.Player));
                    continue;
                }

                if (!ability.Equals(found_ability))
                {
                    dispatcher.Broadcast(new AbilityUpdated(ability, found_ability, evt.Player));
                }
            }

            foreach (var ability in evt.Previous)
            {
                Ability found_ability = evt.New.FirstOrDefault((Ability element) => { return element.Name.Equals(ability.Name); }, null);

                if (found_ability == null)
                {
                    dispatcher.Broadcast(new AbilityRemoved(ability, evt.Player));
                }
            }
        }
    }
}
