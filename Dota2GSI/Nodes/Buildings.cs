using Dota2GSI.Nodes.BuildingsProvider;
using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing buildings.
    /// </summary>
    public class Buildings : Node
    {
        /// <summary>
        /// Gets all buildings layouts.
        /// </summary>
        public readonly NodeMap<PlayerTeam, BuildingLayout> AllBuildings = new NodeMap<PlayerTeam, BuildingLayout>();

        /// <summary>
        /// Gets Radiant buildings layout.
        /// </summary>
        public BuildingLayout RadiantBuildings
        {
            get
            {
                return AllBuildings[PlayerTeam.Radiant];
            }
        }

        /// <summary>
        ///  Gets Dire buildings layout.
        /// </summary>
        public BuildingLayout DireBuildings
        {
            get
            {
                return AllBuildings[PlayerTeam.Dire];
            }
        }

        internal Buildings(JObject parsed_data = null) : base(parsed_data)
        {
            AllBuildings.Add(PlayerTeam.Radiant, new BuildingLayout(GetJObject("radiant")));
            AllBuildings.Add(PlayerTeam.Dire, new BuildingLayout(GetJObject("dire")));
        }

        /// <summary>
        /// Gets building layout for a specific team.<br/>
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>The building layout.</returns>
        public BuildingLayout GetForTeam(PlayerTeam team)
        {
            switch (team)
            {
                case PlayerTeam.Radiant:
                    return RadiantBuildings;
                case PlayerTeam.Dire:
                    return DireBuildings;
                default:
                    break;
            }

            return new BuildingLayout();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"AllBuildings: {AllBuildings}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is Buildings other &&
                AllBuildings.Equals(other.AllBuildings);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 107254384;
            hashCode = hashCode * -739411027 + AllBuildings.GetHashCode();
            return hashCode;
        }
    }
}
