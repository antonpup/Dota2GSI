using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class GameStateHandler : EventHandler<DotaGameEvent>
    {
        private GameState _previous_game_state = new GameState();

        public GameStateHandler(ref EventDispatcher<DotaGameEvent> EventDispatcher) : base(ref EventDispatcher)
        {
        }

        public void OnNewGameState(GameState game_state)
        {
            if (!game_state.IsValid())
            {
                // Invalid game state provided, nothing to do here.
                return;
            }

            if (!_previous_game_state.IsValid() && game_state.Previously.IsValid())
            {
                // Update previous game state cache.
                _previous_game_state = game_state.Previously;
            }

            // Broadcast changes for custom providers.

            if (!_previous_game_state.LocalPlayer.Equals(game_state.LocalPlayer))
            {
                dispatcher.Broadcast(new FullPlayerDetailsUpdated(game_state.LocalPlayer, _previous_game_state.LocalPlayer));
            }

            if (!_previous_game_state.RadiantTeamDetails.Equals(game_state.RadiantTeamDetails))
            {
                dispatcher.Broadcast(new FullTeamDetailsUpdated(game_state.RadiantTeamDetails, _previous_game_state.RadiantTeamDetails));
            }

            if (!_previous_game_state.DireTeamDetails.Equals(game_state.DireTeamDetails))
            {
                dispatcher.Broadcast(new FullTeamDetailsUpdated(game_state.DireTeamDetails, _previous_game_state.DireTeamDetails));
            }

            if (!_previous_game_state.NeutralTeamDetails.Equals(game_state.NeutralTeamDetails))
            {
                dispatcher.Broadcast(new FullTeamDetailsUpdated(game_state.NeutralTeamDetails, _previous_game_state.NeutralTeamDetails));
            }

            // Broadcast changes for providers.

            if (!_previous_game_state.Auth.Equals(game_state.Auth))
            {
                dispatcher.Broadcast(new AuthUpdated(game_state.Auth, _previous_game_state.Auth));
            }

            if (!_previous_game_state.Provider.Equals(game_state.Provider))
            {
                dispatcher.Broadcast(new ProviderUpdated(game_state.Provider, _previous_game_state.Provider));
            }

            if (!_previous_game_state.Map.Equals(game_state.Map))
            {
                dispatcher.Broadcast(new MapUpdated(game_state.Map, _previous_game_state.Map));
            }

            if (!_previous_game_state.Player.Equals(game_state.Player))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new PlayerUpdated(game_state.Player, _previous_game_state.Player));
            }

            if (!_previous_game_state.Hero.Equals(game_state.Hero))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new HeroUpdated(game_state.Hero, _previous_game_state.Hero));
            }

            if (!_previous_game_state.Abilities.Equals(game_state.Abilities))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new AbilitiesUpdated(game_state.Abilities, _previous_game_state.Abilities));
            }

            if (!_previous_game_state.Items.Equals(game_state.Items))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new ItemsUpdated(game_state.Items, _previous_game_state.Items));
            }

            if (!_previous_game_state.Events.Equals(game_state.Events))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new EventsUpdated(game_state.Events, _previous_game_state.Events));
            }

            if (!_previous_game_state.Buildings.Equals(game_state.Buildings))
            {
                dispatcher.Broadcast(new BuildingsUpdated(game_state.Buildings, _previous_game_state.Buildings));
            }

            if (!_previous_game_state.League.Equals(game_state.League))
            {
                dispatcher.Broadcast(new LeagueUpdated(game_state.League, _previous_game_state.League));
            }

            if (!_previous_game_state.Draft.Equals(game_state.Draft))
            {
                dispatcher.Broadcast(new DraftUpdated(game_state.Draft, _previous_game_state.Draft));
            }

            if (!_previous_game_state.Wearables.Equals(game_state.Wearables))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new WearablesUpdated(game_state.Wearables, _previous_game_state.Wearables));
            }

            if (!_previous_game_state.Minimap.Equals(game_state.Minimap))
            {
                dispatcher.Broadcast(new MinimapUpdated(game_state.Minimap, _previous_game_state.Minimap));
            }

            if (!_previous_game_state.Roshan.Equals(game_state.Roshan))
            {
                dispatcher.Broadcast(new RoshanUpdated(game_state.Roshan, _previous_game_state.Roshan));
            }

            if (!_previous_game_state.Couriers.Equals(game_state.Couriers))
            {
                // Depends on FullPlayerDetailsUpdated. This broadcast must happen after FullPlayerDetailsUpdated.
                dispatcher.Broadcast(new CouriersUpdated(game_state.Couriers, _previous_game_state.Couriers));
            }

            if (!_previous_game_state.NeutralItems.Equals(game_state.NeutralItems))
            {
                dispatcher.Broadcast(new NeutralItemsUpdated(game_state.NeutralItems, _previous_game_state.NeutralItems));
            }

            // Finally update the previous game state cache.
            _previous_game_state = game_state;
        }
    }
}
