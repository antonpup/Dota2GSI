using Dota2GSI.Nodes;
using Dota2GSI.Nodes.BuildingsProvider;

namespace Dota2GSI.EventMessages
{
    /// <summary>
    /// Event for overall Buildings update. 
    /// </summary>
    public class BuildingsUpdated : UpdateEvent<Buildings>
    {
        public BuildingsUpdated(Buildings new_value, Buildings previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Building Layout update.
    /// </summary>
    public class BuildingsLayoutUpdated : TeamUpdateEvent<BuildingLayout>
    {
        public BuildingsLayoutUpdated(BuildingLayout new_value, BuildingLayout previous_value, PlayerTeam team) : base(new_value, previous_value, team)
        {
        }
    }

    /// <summary>
    /// Enum for building locations.
    /// </summary>
    public enum BuildingLocation
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Top lane building.
        /// </summary>
        TopLane,

        /// <summary>
        /// Middle lane building.
        /// </summary>
        MiddleLane,

        /// <summary>
        /// Bottom lane building.
        /// </summary>
        BottomLane,

        /// <summary>
        /// Base building.
        /// </summary>
        Base
    }

    /// <summary>
    /// Event for specific Building update.
    /// </summary>
    public class BuildingUpdated : EntityUpdateEvent<Building>
    {
        public BuildingUpdated(Building new_value, Building previous_value, string entity_id) : base(new_value, previous_value, entity_id)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Building update.
    /// </summary>
    public class TeamBuildingUpdated : BuildingUpdated
    {
        public readonly PlayerTeam Team;

        /// <summary>
        /// The location of this building.
        /// </summary>
        public readonly BuildingLocation Location;

        public TeamBuildingUpdated(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id)
        {
            Team = team;
            Location = location;
        }
    }

    /// <summary>
    /// Event for specific team's Building destruction.
    /// </summary>
    public class TeamBuildingDestroyed : TeamBuildingUpdated
    {
        public TeamBuildingDestroyed(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Tower update.
    /// </summary>
    public class TowerUpdated : TeamBuildingUpdated
    {
        public TowerUpdated(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Tower destruction.
    /// </summary>
    public class TowerDestroyed : TeamBuildingDestroyed
    {
        public TowerDestroyed(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Racks update.
    /// </summary>
    public class RacksUpdated : TeamBuildingUpdated
    {
        /// <summary>
        /// The affected racks type.
        /// </summary>
        public readonly RacksType RacksType;

        public RacksUpdated(Building new_value, Building previous_value, RacksType racks_type, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
            RacksType = racks_type;
        }
    }

    /// <summary>
    /// Event for specific team's Racks destruction.
    /// </summary>
    public class RacksDestroyed : TeamBuildingDestroyed
    {
        /// <summary>
        /// The affected racks type.
        /// </summary>
        public readonly RacksType RacksType;

        public RacksDestroyed(Building new_value, Building previous_value, RacksType racks_type, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
            RacksType = racks_type;
        }
    }

    /// <summary>
    /// Event for specific team's Ancient update.
    /// </summary>
    public class AncientUpdated : TeamBuildingUpdated
    {
        public AncientUpdated(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
        }
    }

    /// <summary>
    /// Event for specific team's Ancient destruction.
    /// </summary>
    public class AncientDestroyed : TeamBuildingDestroyed
    {
        public AncientDestroyed(Building new_value, Building previous_value, string entity_id, PlayerTeam team, BuildingLocation location) : base(new_value, previous_value, entity_id, team, location)
        {
        }
    }
}
