using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.NeutralItemsProvider
{
    /// <summary>
    /// Class representing neutral item tier information.
    /// </summary>
    public class NeutralTierInfo : Node
    {
        /// <summary>
        /// The neutral item's tier.
        /// </summary>
        public readonly int Tier;

        /// <summary>
        /// The neutral item's max count for the tier.
        /// </summary>
        public readonly int MaxCount;

        /// <summary>
        /// The neutral item's availability to drop for the tier.
        /// </summary>
        public readonly int DropAfterTime;

        internal NeutralTierInfo(JObject parsed_data = null) : base(parsed_data)
        {
            Tier = GetInt("tier");
            MaxCount = GetInt("max_count");
            DropAfterTime = GetInt("drop_after_time");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Tier: {Tier}, " +
                $"MaxCount: {MaxCount}, " +
                $"DropAfterTime: {DropAfterTime}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is NeutralTierInfo other &&
                Tier == other.Tier &&
                MaxCount == other.MaxCount &&
                DropAfterTime == other.DropAfterTime;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 919325865;
            hashCode = hashCode * -426172320 + Tier.GetHashCode();
            hashCode = hashCode * -426172320 + MaxCount.GetHashCode();
            hashCode = hashCode * -426172320 + DropAfterTime.GetHashCode();
            return hashCode;
        }
    }
}
