using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing ability attributes.
    /// </summary>
    public class Attributes : Node
    {
        /// <summary>
        /// Amount of levels available to spend.
        /// </summary>
        public readonly int Level;

        internal Attributes(JObject parsed_data = null) : base(parsed_data)
        {
            Level = GetInt("level");
        }
    }
}
