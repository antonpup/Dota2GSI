using Dota2GSI.Nodes.AbilitiesProvider;
using Dota2GSI.Nodes.CouriersProvider;
using Dota2GSI.Nodes.HeroProvider;
using Dota2GSI.Nodes.ItemsProvider;
using Dota2GSI.Nodes.MinimapProvider;
using Dota2GSI.Nodes.PlayerProvider;
using Dota2GSI.Nodes.WearablesProvider;

namespace Dota2GSI.Nodes.Helpers
{
    /// <summary>
    /// Class representing full player details composed from GameState data.
    /// </summary>
    public class FullPlayerDetails
    {
        /// <summary>
        /// Player's ID. -1 for local player.
        /// </summary>
        public readonly int PlayerID;

        /// <summary>
        /// True if player details are for the local player.
        /// </summary>
        public bool IsLocalPlayer => PlayerID.Equals(-1);

        /// <summary>
        /// Player's basic details.
        /// </summary>
        public readonly PlayerDetails Details = new PlayerDetails();

        /// <summary>
        /// Player's hero.
        /// </summary>
        public readonly HeroDetails Hero = new HeroDetails();

        /// <summary>
        /// Player's hero abilities.
        /// </summary>
        public readonly AbilityDetails Abilities = new AbilityDetails();

        /// <summary>
        /// Player's items.
        /// </summary>
        public readonly ItemDetails Items = new ItemDetails();

        /// <summary>
        /// Player's wearables.
        /// </summary>
        public readonly PlayerWearables Wearables = new PlayerWearables();

        /// <summary>
        /// Player's courier.
        /// </summary>
        public readonly Courier Courier = new Courier();

        /// <summary>
        /// Player's minimap elements.
        /// </summary>
        public readonly NodeMap<int, MinimapElement> MinimapElements = new NodeMap<int, MinimapElement>();

        internal FullPlayerDetails(int player_id, GameState game_state)
        {
            PlayerID = player_id;
            Details = game_state.Player.GetForPlayer(player_id);
            Hero = game_state.Hero.GetForPlayer(player_id);
            Abilities = game_state.Abilities.GetForPlayer(player_id);
            Items = game_state.Items.GetForPlayer(player_id);
            Wearables = game_state.Wearables.GetForPlayer(player_id);
            Courier = game_state.Couriers.GetForPlayer(player_id);
            MinimapElements = game_state.Minimap.GetByUnitName(Hero.Name);
        }

        internal FullPlayerDetails(GameState game_state)
        {
            PlayerID = -1;
            Details = game_state.Player.LocalPlayer;
            Hero = game_state.Hero.LocalPlayer;
            Abilities = game_state.Abilities.LocalPlayer;
            Items = game_state.Items.LocalPlayer;
            Wearables = game_state.Wearables.LocalPlayer;
            // Courier information is not available for local players.
            MinimapElements = game_state.Minimap.GetByUnitName(Hero.Name);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"PlayerDetails: {Details}, " +
                $"PlayerHero: {Hero}, " +
                $"PlayerAbilities: {Abilities}, " +
                $"PlayerItems: {Items}, " +
                $"PlayerWearables: {Wearables}, " +
                $"PlayerCourier: {Courier}, " +
                $"PlayerMinimapElements: {MinimapElements}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is FullPlayerDetails other &&
                Details.Equals(other.Details) &&
                Hero.Equals(other.Hero) &&
                Abilities.Equals(other.Abilities) &&
                Items.Equals(other.Items) &&
                Wearables.Equals(other.Wearables) &&
                Courier.Equals(other.Courier) &&
                MinimapElements.Equals(other.MinimapElements);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 339357341;
            hashCode = hashCode * -959873209 + Details.GetHashCode();
            hashCode = hashCode * -959873209 + Hero.GetHashCode();
            hashCode = hashCode * -959873209 + Abilities.GetHashCode();
            hashCode = hashCode * -959873209 + Items.GetHashCode();
            hashCode = hashCode * -959873209 + Wearables.GetHashCode();
            hashCode = hashCode * -959873209 + Courier.GetHashCode();
            hashCode = hashCode * -959873209 + MinimapElements.GetHashCode();
            return hashCode;
        }
    }
}
