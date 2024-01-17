using Dota2GSI.EventMessages;
using Dota2GSI.Nodes.Helpers;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class PlayerHandler : EventHandler<DotaGameEvent>
    {
        private Dictionary<int, FullPlayerDetails> _player_cache = new Dictionary<int, FullPlayerDetails>();

        public PlayerHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Subscribe<PlayerUpdated>(OnPlayerStateUpdated);
            dispatcher.Subscribe<PlayerDetailsChanged>(OnPlayerDetailsChanged);
        }

        ~PlayerHandler()
        {
            dispatcher.Unsubscribe<FullPlayerDetailsUpdated>(OnFullPlayerDetailsUpdated);
            dispatcher.Unsubscribe<PlayerUpdated>(OnPlayerStateUpdated);
            dispatcher.Unsubscribe<PlayerDetailsChanged>(OnPlayerDetailsChanged);
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

        private void OnPlayerStateUpdated(DotaGameEvent e)
        {
            PlayerUpdated evt = (e as PlayerUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new PlayerDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer, _player_cache[-1]));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new PlayerDetailsChanged(player_kvp.Value, previous_hero_details, _player_cache[player_kvp.Key]));
                    }
                }
            }
        }

        private void OnPlayerDetailsChanged(DotaGameEvent e)
        {
            PlayerDetailsChanged evt = (e as PlayerDetailsChanged);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.Kills.Equals(evt.Previous.Kills))
            {
                dispatcher.Broadcast(new PlayerKillsChanged(evt.New.Kills, evt.Previous.Kills, evt.Player));
            }

            if (!evt.New.Deaths.Equals(evt.Previous.Deaths))
            {
                dispatcher.Broadcast(new PlayerDeathsChanged(evt.New.Deaths, evt.Previous.Deaths, evt.Player));
            }

            if (!evt.New.Assists.Equals(evt.Previous.Assists))
            {
                dispatcher.Broadcast(new PlayerAssistsChanged(evt.New.Assists, evt.Previous.Assists, evt.Player));
            }

            if (!evt.New.LastHits.Equals(evt.Previous.LastHits))
            {
                dispatcher.Broadcast(new PlayerLastHitsChanged(evt.New.LastHits, evt.Previous.LastHits, evt.Player));
            }

            if (!evt.New.Denies.Equals(evt.Previous.Denies))
            {
                dispatcher.Broadcast(new PlayerDeniesChanged(evt.New.Denies, evt.Previous.Denies, evt.Player));
            }

            if (!evt.New.KillStreak.Equals(evt.Previous.KillStreak))
            {
                dispatcher.Broadcast(new PlayerKillStreakChanged(evt.New.KillStreak, evt.Previous.KillStreak, evt.Player));
            }

            if (!evt.New.Gold.Equals(evt.Previous.Gold))
            {
                dispatcher.Broadcast(new PlayerGoldChanged(evt.New.Gold, evt.Previous.Gold, evt.Player));
            }

            if (!evt.New.Gold.Equals(evt.Previous.Gold))
            {
                dispatcher.Broadcast(new PlayerGoldChanged(evt.New.Gold, evt.Previous.Gold, evt.Player));
            }

            if (!evt.New.WardsPurchased.Equals(evt.Previous.WardsPurchased))
            {
                dispatcher.Broadcast(new PlayerWardsPurchasedChanged(evt.New.WardsPurchased, evt.Previous.WardsPurchased, evt.Player));
            }

            if (!evt.New.WardsPlaced.Equals(evt.Previous.WardsPlaced))
            {
                dispatcher.Broadcast(new PlayerWardsPlacedChanged(evt.New.WardsPlaced, evt.Previous.WardsPlaced, evt.Player));
            }

            if (!evt.New.WardsDestroyed.Equals(evt.Previous.WardsDestroyed))
            {
                dispatcher.Broadcast(new PlayerWardsDestroyedChanged(evt.New.WardsDestroyed, evt.Previous.WardsDestroyed, evt.Player));
            }

            if (!evt.New.RunesActivated.Equals(evt.Previous.RunesActivated))
            {
                dispatcher.Broadcast(new PlayerRunesActivatedChanged(evt.New.RunesActivated, evt.Previous.RunesActivated, evt.Player));
            }

            if (!evt.New.CampsStacked.Equals(evt.Previous.CampsStacked))
            {
                dispatcher.Broadcast(new PlayerCampsStackedChanged(evt.New.CampsStacked, evt.Previous.CampsStacked, evt.Player));
            }
        }
    }
}
