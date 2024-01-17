using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class BuildingsStateHandler : EventHandler<DotaGameEvent>
    {
        public BuildingsStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
            dispatcher.Subscribe<BuildingsUpdated>(OnBuildingsStateUpdated);
            dispatcher.Subscribe<BuildingsLayoutUpdated>(OnBuildingsLayoutUpdated);
        }

        ~BuildingsStateHandler()
        {
            dispatcher.Unsubscribe<BuildingsUpdated>(OnBuildingsStateUpdated);
            dispatcher.Unsubscribe<BuildingsLayoutUpdated>(OnBuildingsLayoutUpdated);
        }

        private void OnBuildingsStateUpdated(DotaGameEvent e)
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

            if (!evt.New.TopTowers.Equals(evt.Previous.TopTowers))
            {
                BuildingLocation location = BuildingLocation.TopLane;

                foreach (var building_kvp in evt.New.TopTowers)
                {
                    if (!evt.Previous.TopTowers.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "TowerAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.TopTowers[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new TowerUpdated(building_kvp.Value, previous_building, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new TowerDestroyed(building_kvp.Value, previous_building, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.MiddleTowers.Equals(evt.Previous.MiddleTowers))
            {
                BuildingLocation location = BuildingLocation.MiddleLane;

                foreach (var building_kvp in evt.New.MiddleTowers)
                {
                    if (!evt.Previous.MiddleTowers.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "TowerAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.MiddleTowers[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new TowerUpdated(building_kvp.Value, previous_building, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new TowerDestroyed(building_kvp.Value, previous_building, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.BottomTowers.Equals(evt.Previous.BottomTowers))
            {
                BuildingLocation location = BuildingLocation.BottomLane;

                foreach (var building_kvp in evt.New.BottomTowers)
                {
                    if (!evt.Previous.BottomTowers.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "TowerAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.BottomTowers[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new TowerUpdated(building_kvp.Value, previous_building, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new TowerDestroyed(building_kvp.Value, previous_building, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.BottomTowers.Equals(evt.Previous.BottomTowers))
            {
                BuildingLocation location = BuildingLocation.BottomLane;

                foreach (var building_kvp in evt.New.BottomTowers)
                {
                    if (!evt.Previous.BottomTowers.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "TowerAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.BottomTowers[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new TowerUpdated(building_kvp.Value, previous_building, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new TowerDestroyed(building_kvp.Value, previous_building, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.TopRacks.Equals(evt.Previous.TopRacks))
            {
                BuildingLocation location = BuildingLocation.TopLane;

                foreach (var building_kvp in evt.New.TopRacks)
                {
                    if (!evt.Previous.TopRacks.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "RacksAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.TopRacks[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new RacksUpdated(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new RacksDestroyed(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.MiddleRacks.Equals(evt.Previous.MiddleRacks))
            {
                BuildingLocation location = BuildingLocation.MiddleLane;

                foreach (var building_kvp in evt.New.MiddleRacks)
                {
                    if (!evt.Previous.MiddleRacks.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "RacksAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.MiddleRacks[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new RacksUpdated(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new RacksDestroyed(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));
                        }
                    }
                }
            }

            if (!evt.New.BottomRacks.Equals(evt.Previous.BottomRacks))
            {
                BuildingLocation location = BuildingLocation.BottomLane;

                foreach (var building_kvp in evt.New.BottomRacks)
                {
                    if (!evt.Previous.BottomRacks.ContainsKey(building_kvp.Key))
                    {
                        // Not much point in having "RacksAdded" event for Dota gameplay.
                        continue;
                    }

                    var previous_building = evt.Previous.BottomRacks[building_kvp.Key];

                    if (!building_kvp.Value.Equals(previous_building))
                    {
                        dispatcher.Broadcast(new RacksUpdated(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new RacksDestroyed(building_kvp.Value, previous_building, building_kvp.Key, "", evt.Team, location));
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

                    if (evt.New.Ancient.Health == 0)
                    {
                        dispatcher.Broadcast(new AncientDestroyed(evt.New.Ancient, evt.Previous.Ancient, "", evt.Team, location));
                    }
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

                        if (building_kvp.Value.Health == 0)
                        {
                            dispatcher.Broadcast(new TeamBuildingDestroyed(building_kvp.Value, previous_building, building_kvp.Key, evt.Team, location));
                        }
                    }
                }
            }
        }
    }
}
