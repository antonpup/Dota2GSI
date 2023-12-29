using System;
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
        private GameState added;

        /// <summary>
        /// Creates a default GameState instance.
        /// </summary>
        public GameState() : this("{}")
        {
        }

        /// <summary>
        /// Creates a GameState instance based on the passed json data.
        /// </summary>
        /// <param name="json_data">The passed json data.</param>
        public GameState(string json_data) : base(json_data)
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
                    auth = new Auth(GetString("auth"));
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
                    provider = new Provider(GetString("provider"));
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
                    map = new Map(GetString("map"));
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
                    JToken player;
                    Regex r = new Regex(@"team\d");
                    if (_ParsedData.TryGetValue("player", out player) && (player.Where(s => (r.IsMatch(((JProperty)s).Name))).ToList().Count > 0))
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
                    player = new Player(GetString("player"));
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
                    hero = new Hero(GetString("hero"));
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
                    abilities = new Abilities(GetString("abilities"));
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
                    items = new Items(GetString("items"));
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
                    previously = new GameState(GetString("previously"));
                }

                return previously;
            }
        }

        /// <summary>
        /// Changes from previous GameState.
        /// </summary>
        public GameState Added
        {
            get
            {
                if (added == null)
                {
                    added = new GameState(GetString("added"));
                }

                return added;
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
