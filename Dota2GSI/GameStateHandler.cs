using Dota2GSI.EventMessages;

namespace Dota2GSI
{
    public class GameStateHandler : EventHandler<DotaGameEvent>
    {
        private GameState previous_game_state = new GameState();

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

            if (!previous_game_state.IsValid() && game_state.Previously.IsValid())
            {
                // Update previous game state cache.
                previous_game_state = game_state.Previously;
            }

            // Broadcast changes for providers.

            if (!previous_game_state.Auth.Equals(game_state.Auth))
            {
                dispatcher.Broadcast(new AuthStateUpdated(game_state.Auth, previous_game_state.Auth));
            }

            if (!previous_game_state.Provider.Equals(game_state.Provider))
            {
                dispatcher.Broadcast(new ProviderStateUpdated(game_state.Provider, previous_game_state.Provider));
            }

            if (!previous_game_state.Map.Equals(game_state.Map))
            {
                dispatcher.Broadcast(new MapStateUpdated(game_state.Map, previous_game_state.Map));
            }

            if (!previous_game_state.Player.Equals(game_state.Player))
            {
                dispatcher.Broadcast(new PlayerStateUpdated(game_state.Player, previous_game_state.Player));
            }

            if (!previous_game_state.Hero.Equals(game_state.Hero))
            {
                dispatcher.Broadcast(new HeroStateUpdated(game_state.Hero, previous_game_state.Hero));
            }

            if (!previous_game_state.Abilities.Equals(game_state.Abilities))
            {
                dispatcher.Broadcast(new AbilitiesStateUpdated(game_state.Abilities, previous_game_state.Abilities));
            }

            if (!previous_game_state.Items.Equals(game_state.Items))
            {
                dispatcher.Broadcast(new ItemsStateUpdated(game_state.Items, previous_game_state.Items));
            }

            if (!previous_game_state.Events.Equals(game_state.Events))
            {
                dispatcher.Broadcast(new EventsStateUpdated(game_state.Events, previous_game_state.Events));
            }

            if (!previous_game_state.Buildings.Equals(game_state.Buildings))
            {
                dispatcher.Broadcast(new BuildingsStateUpdated(game_state.Buildings, previous_game_state.Buildings));
            }

            if (!previous_game_state.League.Equals(game_state.League))
            {
                dispatcher.Broadcast(new LeagueStateUpdated(game_state.League, previous_game_state.League));
            }

            if (!previous_game_state.Draft.Equals(game_state.Draft))
            {
                dispatcher.Broadcast(new DraftStateUpdated(game_state.Draft, previous_game_state.Draft));
            }

            if (!previous_game_state.Wearables.Equals(game_state.Wearables))
            {
                dispatcher.Broadcast(new WearablesStateUpdated(game_state.Wearables, previous_game_state.Wearables));
            }

            if (!previous_game_state.Minimap.Equals(game_state.Minimap))
            {
                dispatcher.Broadcast(new MinimapStateUpdated(game_state.Minimap, previous_game_state.Minimap));
            }

            if (!previous_game_state.Roshan.Equals(game_state.Roshan))
            {
                dispatcher.Broadcast(new RoshanStateUpdated(game_state.Roshan, previous_game_state.Roshan));
            }

            if (!previous_game_state.Couriers.Equals(game_state.Couriers))
            {
                dispatcher.Broadcast(new CouriersStateUpdated(game_state.Couriers, previous_game_state.Couriers));
            }

            if (!previous_game_state.NeutralItems.Equals(game_state.NeutralItems))
            {
                dispatcher.Broadcast(new NeutralItemsStateUpdated(game_state.NeutralItems, previous_game_state.NeutralItems));
            }

            // Finally update the previous game state cache.
            previous_game_state = game_state;
        }
    }
}
