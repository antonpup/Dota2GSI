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

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is CourierItem other &&
                Name.Equals(other.Name) &&
                OwnerID.Equals(other.OwnerID);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 710433606;
            hashCode = hashCode * -324247450 + Name.GetHashCode();
            hashCode = hashCode * -324247450 + OwnerID.GetHashCode();
            return hashCode;
        }
    }
}
