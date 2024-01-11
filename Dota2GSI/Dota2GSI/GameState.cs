using Dota2GSI.Nodes;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

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
        private TeamsGroup teams;
        private Player player;
        private Hero hero;
        private Abilities abilities;
        private Items items;
        private GameState previously;
        // private GameState added; // Added is removed due to only returning bool values instead of proper values.

        /// <summary>
        /// Creates a GameState instance based on the given json data.
        /// </summary>
        /// <param name="parsed_data">The parsed json data.</param>
        public GameState(JObject parsed_data = null) : base(parsed_data)
        {
        }

        /// <summary>
        /// Information about GSI authentication.
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
        /// Information about the provider of this GameState.
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
        /// Information about the current map.
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
        /// Information of all the players in the game. (SPECTATOR ONLY)
        /// </summary>
        public TeamsGroup Teams
        {
            get
            {
                if (teams == null)
                {
                    JToken player = GetJToken("player");
                    Regex r = new Regex(@"team\d");
                    if (player != null && (player.Where(s => (r.IsMatch(((JProperty)s).Name))).ToList().Count > 0))
                    {
                        teams = new TeamsGroup(_ParsedData);
                    }
                }

                return teams;
            }
        }

        /// <summary>
        /// Determines if the a game is being spectated.
        /// </summary>
        public bool IsSpectator { get { return Teams != null; } }

        /// <summary>
        /// Information about the local player.
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
        /// Information about the local player's hero.
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
        /// Information about the local player's hero abilities.
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
        /// Information about the local player's hero items.
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
        /// Returns the json string that generated this GameState instance
        /// </summary>
        /// <returns>Json string</returns>
        public override string ToString()
        {
            return _ParsedData.ToString();
        }
    }
}
