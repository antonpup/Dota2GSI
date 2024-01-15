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
        private Auth auth;
        private Provider provider;
        private Map map;
        private Player player;
        private Hero hero;
        private Abilities abilities;
        private Items items;
        private Events events;
        private Buildings buildings;
        private League league;
        private Draft draft;
        private Wearables wearables;
        private Minimap minimap;
        private Roshan roshan;
        private Couriers couriers;
        private NeutralItems neutral_items;
        private GameState previously;
        // private GameState added; // Added is removed due to only returning bool values instead of proper values.

        // Helpers

        private FullPlayerDetails local_player_details;
        private FullTeamDetails radiant_team_details;
        private FullTeamDetails dire_team_details;
        private FullTeamDetails neutral_team_details;


        /// <summary>
        /// Creates a GameState instance based on the given json data.
        /// </summary>
        /// <param name="parsed_data">The parsed json data.</param>
        public GameState(JObject parsed_data = null) : base(parsed_data)
        {
        }

        /// <summary>
        /// Information about GSI authentication.<br/>
        /// Enabled by including <code>"auth" "1"</code> in the game state cfg file.
        /// </summary>
        public Auth Auth
        {
            get
            {
                if (auth == null)
                {
                    auth = new Auth(GetJObject("auth"));
                }

                return auth;
            }
        }

        /// <summary>
        /// Information about the provider of this GameState.<br/>
        /// Enabled by including <code>"provider" "1"</code> in the game state cfg file.
        /// </summary>
        public Provider Provider
        {
            get
            {
                if (provider == null)
                {
                    provider = new Provider(GetJObject("provider"));
                }

                return provider;
            }
        }

        /// <summary>
        /// Information about the current map.<br/>
        /// Enabled by including <code>"map" "1"</code> in the game state cfg file.
        /// </summary>
        public Map Map
        {
            get
            {
                if (map == null)
                {
                    map = new Map(GetJObject("map"));
                }

                return map;
            }
        }

        /// <summary>
        /// Information about the local player or team players when spectating.<br/>
        /// Enabled by including <code>"player" "1"</code> in the game state cfg file.
        /// </summary>
        public Player Player
        {
            get
            {
                if (player == null)
                {
                    player = new Player(GetJObject("player"));
                }

                return player;
            }
        }

        /// <summary>
        /// Information about the local player's hero or team players heroes when spectating.<br/>
        /// Enabled by including <code>"hero" "1"</code> in the game state cfg file.
        /// </summary>
        public Hero Hero
        {
            get
            {
                if (hero == null)
                {
                    hero = new Hero(GetJObject("hero"));
                }

                return hero;
            }
        }

        /// <summary>
        /// Information about the local player's hero abilities or team players abilities when spectating.<br/>
        /// Enabled by including <code>"abilities" "1"</code> in the game state cfg file.
        /// </summary>
        public Abilities Abilities
        {
            get
            {
                if (abilities == null)
                {
                    abilities = new Abilities(GetJObject("abilities"));
                }

                return abilities;
            }
        }

        /// <summary>
        /// Information about the local player's hero items or team players items when spectating.<br/>
        /// Enabled by including <code>"items" "1"</code> in the game state cfg file.
        /// </summary>
        public Items Items
        {
            get
            {
                if (items == null)
                {
                    items = new Items(GetJObject("items"));
                }

                return items;
            }
        }

        /// <summary>
        /// Information about game events.<br/>
        /// Enabled by including <code>"events" "1"</code> in the game state cfg file.
        /// </summary>
        public Events Events
        {
            get
            {
                if (events == null)
                {
                    events = new Events(GetJArray("events"));
                }

                return events;
            }
        }

        /// <summary>
        /// Information about the buildings on the map.<br/>
        /// Enabled by including <code>"buildings" "1"</code> in the game state cfg file.
        /// </summary>
        public Buildings Buildings
        {
            get
            {
                if (buildings == null)
                {
                    buildings = new Buildings(GetJObject("buildings"));
                }

                return buildings;
            }
        }

        /// <summary>
        /// Information about the current league (or game configuration).<br/>
        /// Enabled by including <code>"league" "1"</code> in the game state cfg file.
        /// </summary>
        public League League
        {
            get
            {
                if (league == null)
                {
                    league = new League(GetJObject("league"));
                }

                return league;
            }
        }

        /// <summary>
        /// Information about the draft. (TOURNAMENT ONLY)<br/>
        /// Enabled by including <code>"draft" "1"</code> in the game state cfg file.
        /// </summary>
        public Draft Draft
        {
            get
            {
                if (draft == null)
                {
                    draft = new Draft(GetJObject("draft"));
                }

                return draft;
            }
        }

        /// <summary>
        /// Information about the local player's wearable items or team players wearable items when spectating.<br/>
        /// Enabled by including <code>"wearables" "1"</code> in the game state cfg file.
        /// </summary>
        public Wearables Wearables
        {
            get
            {
                if (wearables == null)
                {
                    wearables = new Wearables(GetJObject("wearables"));
                }

                return wearables;
            }
        }

        /// <summary>
        /// Information about the minimap.<br/>
        /// Enabled by including <code>"minimap" "1"</code> in the game state cfg file.
        /// </summary>
        public Minimap Minimap
        {
            get
            {
                if (minimap == null)
                {
                    minimap = new Minimap(GetJObject("minimap"));
                }

                return minimap;
            }
        }

        /// <summary>
        /// Information about Roshan. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"roshan" "1"</code> in the game state cfg file.
        /// </summary>
        public Roshan Roshan
        {
            get
            {
                if (roshan == null)
                {
                    roshan = new Roshan(GetJObject("roshan"));
                }

                return roshan;
            }
        }

        /// <summary>
        /// Information about couriers. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"couriers" "1"</code> in the game state cfg file.
        /// </summary>
        public Couriers Couriers
        {
            get
            {
                if (couriers == null)
                {
                    couriers = new Couriers(GetJObject("couriers"));
                }

                return couriers;
            }
        }

        /// <summary>
        /// Information about neutral items. (SPECTATOR ONLY)<br/>
        /// Enabled by including <code>"neutralitems" "1"</code> in the game state cfg file.
        /// </summary>
        public NeutralItems NeutralItems
        {
            get
            {
                if (neutral_items == null)
                {
                    neutral_items = new NeutralItems(GetJObject("neutralitems"));
                }

                return neutral_items;
            }
        }

        /// <summary>
        /// A previous GameState.
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (previously == null)
                {
                    previously = new GameState(GetJObject("previously"));
                }

                return previously;
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
                if (local_player_details == null)
                {
                    local_player_details = new FullPlayerDetails(this);
                }

                return local_player_details;
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
                if (radiant_team_details == null)
                {
                    radiant_team_details = new FullTeamDetails(PlayerTeam.Radiant, this);
                }

                return radiant_team_details;
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
                if (dire_team_details == null)
                {
                    dire_team_details = new FullTeamDetails(PlayerTeam.Dire, this);
                }

                return dire_team_details;
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
                if (neutral_team_details == null)
                {
                    neutral_team_details = new FullTeamDetails(PlayerTeam.Neutrals, this);
                }

                return neutral_team_details;
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
    }
}
