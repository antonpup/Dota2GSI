using Dota2GSI.Nodes.AbilitiesProvider;
using Dota2GSI.Nodes.CouriersProvider;
using Dota2GSI.Nodes.HeroProvider;
using Dota2GSI.Nodes.ItemsProvider;
using Dota2GSI.Nodes.MinimapProvider;
using Dota2GSI.Nodes.PlayerProvider;
using Dota2GSI.Nodes.WearablesProvider;
using System.Collections.Generic;

namespace Dota2GSI.Nodes.Helpers
{
    /// <summary>
    /// Class representing full player details composed from GameState data.
    /// </summary>
    public class FullPlayerDetails
    {
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
        public readonly Dictionary<int, MinimapElement> MinimapElements = new Dictionary<int, MinimapElement>();

        internal FullPlayerDetails(int player_id, GameState game_state)
        {
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
    }
}
