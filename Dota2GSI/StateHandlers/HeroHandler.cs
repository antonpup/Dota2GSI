using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class HeroHandler : EventHandler<DotaGameEvent>
    {
        public HeroHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<HeroUpdated>(OnHeroStateUpdated);
            dispatcher.Subscribe<HeroDetailsChanged>(OnHeroDetailsChanged);
        }

        ~HeroHandler()
        {
            dispatcher.Unsubscribe<HeroUpdated>(OnHeroStateUpdated);
            dispatcher.Unsubscribe<HeroDetailsChanged>(OnHeroDetailsChanged);
        }

        private void OnHeroStateUpdated(DotaGameEvent e)
        {
            HeroUpdated evt = (e as HeroUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new HeroDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new HeroDetailsChanged(player_kvp.Value, previous_hero_details, player_kvp.Key));
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
                dispatcher.Broadcast(new HeroLevelChanged(evt.New.Level, evt.Previous.Level, evt.PlayerID));
            }

            if (!evt.New.Health.Equals(evt.Previous.Health))
            {
                dispatcher.Broadcast(new HeroHealthChanged(evt.New.Health, evt.Previous.Health, evt.PlayerID));

                if (!evt.New.IsAlive && evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroDied(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }
                else if (evt.New.IsAlive && !evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroRespawned(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }
                else if (evt.New.IsAlive && evt.Previous.IsAlive)
                {
                    dispatcher.Broadcast(new HeroTookDamage(evt.New.Health, evt.Previous.Health, evt.PlayerID));
                }
            }

            if (!evt.New.Mana.Equals(evt.Previous.Mana))
            {
                dispatcher.Broadcast(new HeroManaChanged(evt.New.Mana, evt.Previous.Mana, evt.PlayerID));
            }

            if (!evt.New.HeroState.Equals(evt.Previous.HeroState))
            {
                dispatcher.Broadcast(new HeroStateChanged(evt.New.HeroState, evt.Previous.HeroState, evt.PlayerID));
            }

            if (!evt.New.IsMuted.Equals(evt.Previous.IsMuted))
            {
                dispatcher.Broadcast(new HeroMuteStateChanged(evt.New.IsMuted, evt.Previous.IsMuted, evt.PlayerID));
            }

            if (!evt.New.SelectedUnit.Equals(evt.Previous.SelectedUnit))
            {
                dispatcher.Broadcast(new HeroSelectedChanged(evt.New.SelectedUnit, evt.Previous.SelectedUnit, evt.PlayerID));
            }

            if (!evt.New.TalentTree.Equals(evt.Previous.TalentTree))
            {
                dispatcher.Broadcast(new HeroTalentTreeChanged(evt.New.TalentTree, evt.Previous.TalentTree, evt.PlayerID));
            }

            if (!evt.New.AttributesLevel.Equals(evt.Previous.AttributesLevel))
            {
                dispatcher.Broadcast(new HeroAttributesLevelChanged(evt.New.AttributesLevel, evt.Previous.AttributesLevel, evt.PlayerID));
            }
        }
    }
}
