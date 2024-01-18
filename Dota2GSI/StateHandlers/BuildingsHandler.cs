using Dota2GSI.EventMessages;
using Dota2GSI.Nodes;
using Dota2GSI.Nodes.BuildingsProvider;
using System;
using System.Collections.Generic;

namespace Dota2GSI
{
    public class BuildingsHandler : EventHandler<DotaGameEvent>
    {
        public BuildingsHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<BuildingsUpdated>(OnBuildingsUpdated);
            dispatcher.Subscribe<BuildingsLayoutUpdated>(OnBuildingsLayoutUpdated);
            dispatcher.Subscribe<TowerUpdated>(OnTowerUpdated);
            dispatcher.Subscribe<RacksUpdated>(OnRacksUpdated);
            dispatcher.Subscribe<AncientUpdated>(OnAncientUpdated);
            dispatcher.Subscribe<TeamBuildingUpdated>(OnTeamBuildingUpdated);
        }

        ~BuildingsHandler()
        {
            dispatcher.Unsubscribe<BuildingsUpdated>(OnBuildingsUpdated);
            dispatcher.Unsubscribe<BuildingsLayoutUpdated>(OnBuildingsLayoutUpdated);
            dispatcher.Unsubscribe<TowerUpdated>(OnTowerUpdated);
            dispatcher.Unsubscribe<RacksUpdated>(OnRacksUpdated);
            dispatcher.Unsubscribe<AncientUpdated>(OnAncientUpdated);
            dispatcher.Unsubscribe<TeamBuildingUpdated>(OnTeamBuildingUpdated);
        }

        private void OnBuildingsUpdated(DotaGameEvent e)
        {
            BuildingsUpdated evt = (e as BuildingsUpdated);

            if (evt == null)
            {
                return;
            }

            foreach (var building_kvp in evt.New.AllBuildings)
            {
                if (!evt.Previous.AllBuildings.ContainsKey(building_kvp.Key))
                {
                    continue;
                }

                if (!building_kvp.Value.Equals(evt.Previous.AllBuildings[building_kvp.Key]))
                {
                    dispatcher.Broadcast(new BuildingsLayoutUpdated(building_kvp.Value, evt.Previous.AllBuildings[building_kvp.Key], building_kvp.Key));
                }
            }
        }

        private void OnBuildingsLayoutUpdated(DotaGameEvent e)
        {
            BuildingsLayoutUpdated evt = (e as BuildingsLayoutUpdated);

            if (evt == null)
            {
                return;
            }

            Dictionary<BuildingLocation, Tuple<NodeMap<int, Building>, NodeMap<int, Building>>> towers = new Dictionary<BuildingLocation, Tuple<NodeMap<int, Building>, NodeMap<int, Building>>>()
            {
                { BuildingLocation.TopLane, new Tuple<NodeMap<int, Building>, NodeMap<int, Building>>(evt.New.TopTowers, evt.Previous.TopTowers) },
                { BuildingLocation.MiddleLane, new Tuple<NodeMap<int, Building>, NodeMap<int, Building>>(evt.New.MiddleTowers, evt.Previous.MiddleTowers) },
                { BuildingLocation.BottomLane, new Tuple<NodeMap<int, Building>, NodeMap<int, Building>>(evt.New.BottomTowers, evt.Previous.BottomTowers) }
            };

            foreach (var tower_kvp in towers)
            {
                if (!tower_kvp.Value.Item1.Equals(tower_kvp.Value.Item2))
                {
                    foreach (var building_kvp in tower_kvp.Value.Item1)
                    {
                        if (!tower_kvp.Value.Item2.ContainsKey(building_kvp.Key))
                        {
                            // Not much point in having "TowerAdded" event for Dota gameplay.
                            continue;
                        }

                        var previous_building = tower_kvp.Value.Item2[building_kvp.Key];

                        if (!building_kvp.Value.Equals(previous_building))
                        {
                            dispatcher.Broadcast(new TowerUpdated(building_kvp.Value, previous_building, "", evt.Team, tower_kvp.Key));
                        }
                    }
                }
            }

            Dictionary<BuildingLocation, Tuple<NodeMap<RacksType, Building>, NodeMap<RacksType, Building>>> racks = new Dictionary<BuildingLocation, Tuple<NodeMap<RacksType, Building>, NodeMap<RacksType, Building>>>()
            {
                { BuildingLocation.TopLane, new Tuple<NodeMap<RacksType, Building>, NodeMap<RacksType, Building>>(evt.New.TopRacks, evt.Previous.TopRacks) },
                { BuildingLocation.MiddleLane, new Tuple<NodeMap<RacksType, Building>, NodeMap<RacksType, Building>>(evt.New.MiddleRacks, evt.Previous.MiddleRacks) },
                { BuildingLocation.BottomLane, new Tuple<NodeMap<RacksType, Building>, NodeMap<RacksType, Building>>(evt.New.BottomRacks, evt.Previous.BottomRacks) }
            };

            foreach (var rack_kvp in racks)
            {
                if (!rack_kvp.Value.Item1.Equals(rack_kvp.Value.Item2))
                {
                    foreach (var building_kvp in rack_kvp.Value.Item1)
                    {
                        if (!rack_kvp.Value.Item2.ContainsKey(building_kvp.Key))
                        {
                            // Not much point in having "RacksAdded" event for Dota gameplay.
                            continue;
                        }

                        var previous_building = rack_kvp.Value.Item2[building_kvp.Key];

                        if (!building_kvp.Value.Equals(previous_building))
                        {
                            dispatcher.Broadcast(new RacksUpdated(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, rack_kvp.Key));
                        }
                    }
                }
            }

            if (!evt.New.Ancient.Equals(evt.Previous.Ancient))
            {
                BuildingLocation location = BuildingLocation.Base;

                if (!evt.New.Ancient.Equals(evt.Previous.Ancient))
                {
                    dispatcher.Broadcast(new AncientUpdated(evt.New.Ancient, evt.Previous.Ancient, "", evt.Team, location));
                }
            }

            if (!evt.New.OtherBuildings.Equals(evt.Previous.OtherBuildings))
            {
                BuildingLocation location = BuildingLocation.Undefined;

                foreach (var building_kvp in evt.New.OtherBuildings)
                {
                    if (!evt.Previous.OtherBuildings.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "OtherBuildingsAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.OtherBuildings[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new TeamBuildingUpdated(building_kvp.Value, previous_building, building_kvp.Key, evt.Team, location));
                    }
                }
            }
        }

        private void OnTowerUpdated(DotaGameEvent e)
        {
            TowerUpdated evt = (e as TowerUpdated);

            if (evt == null)
            {
                return;
            }

            if (evt.New.Health == 0 && evt.Previous.Health > 0)
            {
                dispatcher.Broadcast(new TowerDestroyed(evt.New, evt.Previous, evt.EntityID, evt.Team, evt.Location));
            }
        }

        private void OnRacksUpdated(DotaGameEvent e)
        {
            RacksUpdated evt = (e as RacksUpdated);

            if (evt == null)
            {
                return;
            }

            if (evt.New.Health == 0 && evt.Previous.Health > 0)
            {
                dispatcher.Broadcast(new RacksDestroyed(evt.New, evt.Previous, evt.RacksType, evt.EntityID, evt.Team, evt.Location));
            }
        }

        private void OnAncientUpdated(DotaGameEvent e)
        {
            AncientUpdated evt = (e as AncientUpdated);

            if (evt == null)
            {
                return;
            }

            if (evt.New.Health == 0 && evt.Previous.Health > 0)
            {
                dispatcher.Broadcast(new AncientDestroyed(evt.New, evt.Previous, evt.EntityID, evt.Team, evt.Location));
            }
        }

        private void OnTeamBuildingUpdated(DotaGameEvent e)
        {
            TeamBuildingUpdated evt = (e as TeamBuildingUpdated);

            if (evt == null)
            {
                return;
            }

            if (evt.New.Health == 0 && evt.Previous.Health > 0)
            {
                dispatcher.Broadcast(new TeamBuildingDestroyed(evt.New, evt.Previous, evt.EntityID, evt.Team, evt.Location));
            }
        }
    }
}
