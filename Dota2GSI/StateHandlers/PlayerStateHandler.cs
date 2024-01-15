using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class PlayerStateHandler : EventHandler<DotaGameEvent>
    {
        public PlayerStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<PlayerStateUpdated>(OnPlayerStateUpdated);
            dispatcher.Subscribe<PlayerDetailsChanged>(OnPlayerDetailsChanged);
        }

        ~PlayerStateHandler()
        {
            dispatcher.Unsubscribe<PlayerStateUpdated>(OnPlayerStateUpdated);
            dispatcher.Unsubscribe<PlayerDetailsChanged>(OnPlayerDetailsChanged);
        }

        private void OnPlayerStateUpdated(DotaGameEvent e)
        {
            PlayerStateUpdated evt = (e as PlayerStateUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.LocalPlayer.Equals(evt.Previous.LocalPlayer))
            {
                dispatcher.Broadcast(new PlayerDetailsChanged(evt.New.LocalPlayer, evt.Previous.LocalPlayer));
            }

            foreach (var team_kvp in evt.New.Teams)
            {
                foreach (var player_kvp in team_kvp.Value)
                {
                    var previous_hero_details = evt.Previous.GetForPlayer(player_kvp.Key);

                    if (!player_kvp.Value.Equals(previous_hero_details))
                    {
                        dispatcher.Broadcast(new PlayerDetailsChanged(player_kvp.Value, previous_hero_details, player_kvp.Key));
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
                dispatcher.Broadcast(new PlayerKillsChanged(evt.New.Kills, evt.Previous.Kills, evt.PlayerID));
            }

            if (!evt.New.Deaths.Equals(evt.Previous.Deaths))
            {
                dispatcher.Broadcast(new PlayerDeathsChanged(evt.New.Deaths, evt.Previous.Deaths, evt.PlayerID));
            }

            if (!evt.New.Assists.Equals(evt.Previous.Assists))
            {
                dispatcher.Broadcast(new PlayerAssistsChanged(evt.New.Assists, evt.Previous.Assists, evt.PlayerID));
            }

            if (!evt.New.LastHits.Equals(evt.Previous.LastHits))
            {
                dispatcher.Broadcast(new PlayerLastHitsChanged(evt.New.LastHits, evt.Previous.LastHits, evt.PlayerID));
            }

            if (!evt.New.Denies.Equals(evt.Previous.Denies))
            {
                dispatcher.Broadcast(new PlayerDeniesChanged(evt.New.Denies, evt.Previous.Denies, evt.PlayerID));
            }

            if (!evt.New.KillStreak.Equals(evt.Previous.KillStreak))
            {
                dispatcher.Broadcast(new PlayerKillStreakChanged(evt.New.KillStreak, evt.Previous.KillStreak, evt.PlayerID));
            }

            if (!evt.New.Gold.Equals(evt.Previous.Gold))
            {
                dispatcher.Broadcast(new PlayerGoldChanged(evt.New.Gold, evt.Previous.Gold, evt.PlayerID));
            }

            if (!evt.New.Gold.Equals(evt.Previous.Gold))
            {
                dispatcher.Broadcast(new PlayerGoldChanged(evt.New.Gold, evt.Previous.Gold, evt.PlayerID));
            }

            if (!evt.New.WardsPurchased.Equals(evt.Previous.WardsPurchased))
            {
                dispatcher.Broadcast(new PlayerWardsPurchasedChanged(evt.New.WardsPurchased, evt.Previous.WardsPurchased, evt.PlayerID));
            }

            if (!evt.New.WardsPlaced.Equals(evt.Previous.WardsPlaced))
            {
                dispatcher.Broadcast(new PlayerWardsPlacedChanged(evt.New.WardsPlaced, evt.Previous.WardsPlaced, evt.PlayerID));
            }

            if (!evt.New.WardsDestroyed.Equals(evt.Previous.WardsDestroyed))
            {
                dispatcher.Broadcast(new PlayerWardsDestroyedChanged(evt.New.WardsDestroyed, evt.Previous.WardsDestroyed, evt.PlayerID));
            }

            if (!evt.New.RunesActivated.Equals(evt.Previous.RunesActivated))
            {
                dispatcher.Broadcast(new PlayerRunesActivatedChanged(evt.New.RunesActivated, evt.Previous.RunesActivated, evt.PlayerID));
            }

            if (!evt.New.CampsStacked.Equals(evt.Previous.CampsStacked))
            {
                dispatcher.Broadcast(new PlayerCampsStackedChanged(evt.New.CampsStacked, evt.Previous.CampsStacked, evt.PlayerID));
            }
        }
    }
}
