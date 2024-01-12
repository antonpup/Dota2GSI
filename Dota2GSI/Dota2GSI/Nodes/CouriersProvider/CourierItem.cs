using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.CouriersProvider
{
    /// <summary>
    /// Class representing a courier item.
    /// </summary>
    public class CourierItem : Node
    {
        /// <summary>
        /// Item's owner ID.
        /// </summary>
        public readonly int OwnerID;

        /// <summary>
        /// Item's name.
        /// </summary>
        public readonly string Name;

        internal CourierItem(JObject parsed_data = null) : base(parsed_data)
        {
            OwnerID = GetInt("owner");
            Name = GetString("name");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"OwnerID: {OwnerID}" +
                $"]";
        }
    }
}
