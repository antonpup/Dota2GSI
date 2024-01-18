using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes.NeutralItemsProvider
{
    /// <summary>
    /// Enum for different states of neutral items.
    /// </summary>
    public enum NeutralItemState
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = -1,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// In stash.
        /// </summary>
        Stash,

        /// <summary>
        /// Consumed.
        /// </summary>
        Consumed,

        /// <summary>
        /// Equipped.
        /// </summary>
        Equipped,

        /// <summary>
        /// In backpack.
        /// </summary>
        Backpack,

        /// <summary>
        /// In player's stash.
        /// </summary>
        Player_Stash,

        /// <summary>
        /// On courier.
        /// </summary>
        Courier
    }

    /// <summary>
    /// Class representing neutral item information.
    /// </summary>
    public class NeutralItem : Node
    {
        /// <summary>
        /// The neutral item's name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The neutral item's name.
        /// </summary>
        public readonly int Tier;

        /// <summary>
        /// The neutral item's charges.
        /// </summary>
        public readonly int Charges;

        /// <summary>
        /// The neutral item's state.
        /// </summary>
        public readonly NeutralItemState State;

        /// <summary>
        /// The neutral item's possessing player ID.
        /// </summary>
        public readonly int PlayerID;

        internal NeutralItem(JObject parsed_data = null) : base(parsed_data)
        {
            Name = GetString("name");
            Tier = GetInt("tier");
            Charges = GetInt("charges");
            State = GetEnum<NeutralItemState>("state");
            PlayerID = GetInt("player_id");
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Name: {Name}, " +
                $"Tier: {Tier}, " +
                $"Charges: {Charges}, " +
                $"State: {State}, " +
                $"PlayerID: {PlayerID}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is NeutralItem other &&
                Name.Equals(other.Name) &&
                Tier.Equals(other.Tier) &&
                Charges.Equals(other.Charges) &&
                State.Equals(other.State) &&
                PlayerID.Equals(other.PlayerID);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 905733096;
            hashCode = hashCode * -66160412 + Name.GetHashCode();
            hashCode = hashCode * -66160412 + Tier.GetHashCode();
            hashCode = hashCode * -66160412 + Charges.GetHashCode();
            hashCode = hashCode * -66160412 + State.GetHashCode();
            hashCode = hashCode * -66160412 + PlayerID.GetHashCode();
            return hashCode;
        }
    }
}
