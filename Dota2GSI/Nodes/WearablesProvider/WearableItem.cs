
namespace Dota2GSI.Nodes.WearablesProvider
{
    /// <summary>
    /// Class representing wearable item information.
    /// </summary>
    public class WearableItem
    {
        /// <summary>
        /// The ID of the wearable item.
        /// </summary>
        public readonly int ID;

        /// <summary>
        /// The style of the wearable item.
        /// </summary>
        public readonly int Style;

        internal WearableItem(int id = 0, int style = 0)
        {
            ID = id;
            Style = style;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"ID: {ID}, " +
                $"Style: {Style}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is WearableItem other &&
                ID.Equals(other.ID) &&
                Style.Equals(other.Style);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 652470603;
            hashCode = hashCode * -397083684 + ID.GetHashCode();
            hashCode = hashCode * -397083684 + Style.GetHashCode();
            return hashCode;
        }
    }
}
