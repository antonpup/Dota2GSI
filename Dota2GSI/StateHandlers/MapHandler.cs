﻿using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class MapHandler : EventHandler<DotaGameEvent>
    {
        public MapHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<MapUpdated>(OnMapUpdated);
        }

        ~MapHandler()
        {
            dispatcher.Unsubscribe<MapUpdated>(OnMapUpdated);
        }

        private void OnMapUpdated(DotaGameEvent e)
        {
            MapUpdated evt = (e as MapUpdated);

            if (evt == null)
            {
                return;
            }

            if (!evt.New.IsDaytime.Equals(evt.Previous.IsDaytime) || !evt.New.IsNightstalkerNight.Equals(evt.Previous.IsNightstalkerNight))
            {
                dispatcher.Broadcast(new TimeOfDayChanged(evt.New.IsDaytime, evt.New.IsNightstalkerNight));
            }

            if (!evt.New.RadiantScore.Equals(evt.Previous.RadiantScore))
            {
                dispatcher.Broadcast(new TeamScoreChanged(evt.New.RadiantScore, evt.Previous.RadiantScore, Nodes.PlayerTeam.Radiant));
            }

            if (!evt.New.DireScore.Equals(evt.Previous.DireScore))
            {
                dispatcher.Broadcast(new TeamScoreChanged(evt.New.DireScore, evt.Previous.DireScore, Nodes.PlayerTeam.Dire));
            }

            if (!evt.New.GameState.Equals(evt.Previous.GameState))
            {
                dispatcher.Broadcast(new GameStateChanged(evt.New.GameState, evt.Previous.GameState));
            }

            if (!evt.New.IsPaused.Equals(evt.Previous.IsPaused))
            {
                dispatcher.Broadcast(new PauseStateChanged(evt.New.IsPaused, evt.Previous.IsPaused));

                if (evt.New.IsPaused)
                {
                    dispatcher.Broadcast(new GamePaused());
                }
                else
                {
                    dispatcher.Broadcast(new GameResumed());
                }
            }

            if (!evt.New.WinningTeam.Equals(evt.Previous.WinningTeam) && evt.New.WinningTeam != Nodes.PlayerTeam.None && evt.New.WinningTeam != Nodes.PlayerTeam.Undefined)
            {
                dispatcher.Broadcast(new TeamVictory(evt.New.WinningTeam));

                switch (evt.New.WinningTeam)
                {
                    case Nodes.PlayerTeam.Radiant:
                        dispatcher.Broadcast(new TeamDefeat(Nodes.PlayerTeam.Dire));
                        break;
                    case Nodes.PlayerTeam.Dire:
                        dispatcher.Broadcast(new TeamDefeat(Nodes.PlayerTeam.Radiant));
                        break;
                }
            }

            if (!evt.New.RoshanState.Equals(evt.Previous.RoshanState))
            {
                dispatcher.Broadcast(new RoshanStateChanged(evt.New.RoshanState, evt.Previous.RoshanState));
            }
        }
    }
}
