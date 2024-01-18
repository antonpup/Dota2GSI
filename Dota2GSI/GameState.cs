using Dota2GSI.Nodes;
using Dota2GSI.Nodes.Helpers;
using Newtonsoft.Json.Linq;

namespace Dota2GSI
{
    /// <summary>
    /// A class representing various information pertaining to Game State Integration of Dota 2.
    /// </summary>
    public class GameState : Node
    {
        /// <summary>
        /// Information about GSI authentication.<br/>
        /// Enabled by including <code>"auth" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Auth Auth;

        /// <summary>
        /// Information about the provider of this GameState.<br/>
        /// Enabled by including <code>"provider" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Provider Provider;

        /// <summary>
        /// Information about the current map.<br/>
        /// Enabled by including <code>"map" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Map Map;

        /// <summary>
        /// Information about the local player or team players when spectating.<br/>
        /// Enabled by including <code>"player" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Player Player;

        /// <summary>
        /// Information about the local player's hero or team players heroes when spectating.<br/>
        /// Enabled by including <code>"hero" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Hero Hero;

        /// <summary>
        /// Information about the local player's hero abilities or team players abilities when spectating.<br/>
        /// Enabled by including <code>"abilities" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Abilities Abilities;

        /// <summary>
        /// Information about the local player's hero items or team players items when spectating.<br/>
        /// Enabled by including <code>"items" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Items Items;

        /// <summary>
        /// Information about game events.<br/>
        /// Enabled by including <code>"events" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Events Events;

        /// <summary>
        /// Information about the buildings on the map.<br/>
        /// Enabled by including <code>"buildings" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Buildings Buildings;

        /// <summary>
        /// Information about the current league (or game configuration).<br/>
        /// Enabled by including <code>"league" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly League League;

        /// <summary>
        /// Information about the draft. (TOURNAMENT ONLY)<br/>
        /// Enabled by including <code>"draft" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Draft Draft;

        /// <summary>
        /// Information about the local player's wearable items or team players wearable items when spectating.<br/>
        /// Enabled by including <code>"wearables" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Wearables Wearables;

        /// <summary>
        /// Information about the minimap.<br/>
        /// Enabled by including <code>"minimap" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Minimap Minimap;

        /// <summary>
        /// Information about Roshan. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"roshan" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Roshan Roshan;

        /// <summary>
        /// Information about couriers. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"couriers" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly Couriers Couriers;

        /// <summary>
        /// Information about neutral items. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"neutralitems" "1"</code> in the game state cfg file.
        /// </summary>
        public readonly NeutralItems NeutralItems;

        /// <summary>
        /// A previous GameState.
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (_previous_game_state == null)
                {
                    _previous_game_state = new GameState(GetJObject("previously"));
                }

                return _previous_game_state;
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Local player details derived from this game state.
        /// </summary>
        public FullPlayerDetails LocalPlayer
        {
            get
            {
                if (_local_player_details == null)
                {
                    _local_player_details = new FullPlayerDetails(this);
                }

                return _local_player_details;
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Radiant team details derived from this game state.
        /// </summary>
        public FullTeamDetails RadiantTeamDetails
        {
            get
            {
                if (_radiant_team_details == null)
                {
                    _radiant_team_details = new FullTeamDetails(PlayerTeam.Radiant, this);
                }

                return _radiant_team_details;
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Dire team details derived from this game state.
        /// </summary>
        public FullTeamDetails DireTeamDetails
        {
            get
            {
                if (_dire_team_details == null)
                {
                    _dire_team_details = new FullTeamDetails(PlayerTeam.Dire, this);
                }

                return _dire_team_details;
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Neutral team details derived from this game state.
        /// </summary>
        public FullTeamDetails NeutralTeamDetails
        {
            get
            {
                if (_neutral_team_details == null)
                {
                    _neutral_team_details = new FullTeamDetails(PlayerTeam.Neutrals, this);
                }

                return _neutral_team_details;
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Is the game client spectating a game?
        /// True if spectating, false otherwise.
        /// </summary>
        public bool IsSpectating
        {
            get
            {
                return Player.IsValid() && !Player.LocalPlayer.IsValid() && (Player.Teams.Count > 0);
            }
        }

        /// <summary>
        /// Helper variable,<br/>
        /// Is the game client playing a game?
        /// True if local player is playing a game, false otherwise.
        /// </summary>
        public bool IsLocalPlayer
        {
            get
            {
                return Player.IsValid() && Player.LocalPlayer.IsValid() && (Player.Teams.Count == 0);
            }
        }

        private GameState _previous_game_state;

        // Helpers

        private FullPlayerDetails _local_player_details;
        private FullTeamDetails _radiant_team_details;
        private FullTeamDetails _dire_team_details;
        private FullTeamDetails _neutral_team_details;


        /// <summary>
        /// Creates a GameState instance based on the given json data.
        /// </summary>
        /// <param name="parsed_data">The parsed json data.</param>
        public GameState(JObject parsed_data = null) : base(parsed_data)
        {
            Auth = new Auth(GetJObject("auth"));
            Provider = new Provider(GetJObject("provider"));
            Map = new Map(GetJObject("map"));
            Player = new Player(GetJObject("player"));
            Hero = new Hero(GetJObject("hero"));
            Abilities = new Abilities(GetJObject("abilities"));
            Items = new Items(GetJObject("items"));
            Events = new Events(GetJArray("events"));
            Buildings = new Buildings(GetJObject("buildings"));
            League = new League(GetJObject("league"));
            Draft = new Draft(GetJObject("draft"));
            Wearables = new Wearables(GetJObject("wearables"));
            Minimap = new Minimap(GetJObject("minimap"));
            Roshan = new Roshan(GetJObject("roshan"));
            Couriers = new Couriers(GetJObject("couriers"));
            NeutralItems = new NeutralItems(GetJObject("neutralitems"));
        }
    }
}
