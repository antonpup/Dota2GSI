using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class HeroHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public HeroHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<HeroUpdated>(OnHeroUpdated);
            dispatcher.Subscribe<HeroDetailsChanged>(OnHeroDetailsChanged);
        }

        ~HeroHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<HeroUpdated>(OnHeroUpdated);
            dispatcher.Unsubscribe<HeroDetailsChanged>(OnHeroDetailsChanged);
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

        private void OnHeroUpdated(DotaGameEvent e)
        {
            HeroUpdated evt = (e as HeroUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new HeroDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer, _player_cache[-1]));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new HeroDetailsChanged(player_kvp.Value, previous_hero_details, _player_cache[player_kvp.Key]));
                    }
                }
            }
        }

        private void OnHeroDetailsChanged(DotaGameEvent e)
        {
            HeroDetailsChanged evt = (e as HeroDetailsChanged);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Level.Equals(evt.Previous.Level))
            {
                dispatcher.Broadcast(new HeroLevelChanged(evt.New.Level, evt.Previous.Level, evt.Player));
            }

            if (!evt.New.Health.Equals(evt.Previous.Health))
            {
                dispatcher.Broadcast(new HeroHealthChanged(evt.New.Health, evt.Previous.Health, evt.Player));

                if (!evt.New.IsAlive && evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroDied(evt.New.Health, evt.Previous.Health, evt.Player));
                }
                else if (evt.New.IsAlive && !evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroRespawned(evt.New.Health, evt.Previous.Health, evt.Player));
                }
                else if (evt.New.IsAlive && evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroTookDamage(evt.New.Health, evt.Previous.Health, evt.Player));
                }
            }

            if (!evt.New.Mana.Equals(evt.Previous.Mana))
            {
                dispatcher.Broadcast(new HeroManaChanged(evt.New.Mana, evt.Previous.Mana, evt.Player));
            }

            if (!evt.New.HeroState.Equals(evt.Previous.HeroState))
            {
                dispatcher.Broadcast(new HeroStateChanged(evt.New.HeroState, evt.Previous.HeroState, evt.Player));
            }

            if (!evt.New.IsMuted.Equals(evt.Previous.IsMuted))
            {
                dispatcher.Broadcast(new HeroMuteStateChanged(evt.New.IsMuted, evt.Previous.IsMuted, evt.Player));
            }

            if (!evt.New.SelectedUnit.Equals(evt.Previous.SelectedUnit))
            {
                dispatcher.Broadcast(new HeroSelectedChanged(evt.New.SelectedUnit, evt.Previous.SelectedUnit, evt.Player));
            }

            if (!evt.New.TalentTree.Equals(evt.Previous.TalentTree))
            {
                dispatcher.Broadcast(new HeroTalentTreeChanged(evt.New.TalentTree, evt.Previous.TalentTree, evt.Player));
            }

            if (!evt.New.AttributesLevel.Equals(evt.Previous.AttributesLevel))
            {
                dispatcher.Broadcast(new HeroAttributesLevelChanged(evt.New.AttributesLevel, evt.Previous.AttributesLevel, evt.Player));
            }
        }
    }
}
