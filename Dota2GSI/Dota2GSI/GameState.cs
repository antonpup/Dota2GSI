using System;
using Dota2GSI.Nodes;

namespace Dota2GSI
{
    /// <summary>
    /// A class representing various information retaining to Game State Integration of Dota 2
    /// </summary>
    public class GameState
    {
        private Newtonsoft.Json.Linq.JObject _ParsedData;
        private string json;

        private Auth auth;
        private Provider provider;
        private Map map;
        private Player player;
        private Hero hero;
        private Abilities abilities;
        private Items items;
        private GameState previously;
        private GameState added;

        /// <summary>
        /// Creates a GameState instance based on the passed json data.
        /// </summary>
        /// <param name="json_data">The passed json data</param>
        public GameState(string json_data)
        {
            if (json_data.Equals(""))
            {
                json_data = "{}";
            }

            json = json_data;
            _ParsedData = Newtonsoft.Json.Linq.JObject.Parse(json_data);
        }

        /// <summary>
        /// Information about GSI authentication
        /// </summary>
        public Auth Auth
        {
            get
            {
                if (auth == null)
                    auth = new Auth(GetNode("auth"));

                return auth;
            }
        }

        /// <summary>
        /// Information about the provider of this GameState
        /// </summary>
        public Provider Provider
        {
            get
            {
                if (provider == null)
                    provider = new Provider(GetNode("provider"));

                return provider;
            }
        }

        /// <summary>
        /// Information about the current map
        /// </summary>
        public Map Map
        {
            get
            {
                if (map == null)
                    map = new Map(GetNode("map"));

                return map;
            }
        }

        /// <summary>
        /// Information about the local player
        /// </summary>
        public Player Player
        {
            get
            {
                if (player == null)
                    player = new Player(GetNode("player"));

                return player;
            }
        }

        /// <summary>
        /// Information about the local player's hero
        /// </summary>
        public Hero Hero
        {
            get
            {
                if (hero == null)
                    hero = new Hero(GetNode("hero"));

                return hero;
            }
        }

        /// <summary>
        /// Information about the local player's hero abilities
        /// </summary>
        public Abilities Abilities
        {
            get
            {
                if (abilities == null)
                    abilities = new Abilities(GetNode("abilities"));

                return abilities;
            }
        }

        /// <summary>
        /// Information about the local player's hero items
        /// </summary>
        public Items Items
        {
            get
            {
                if (items == null)
                    items = new Items(GetNode("items"));

                return items;
            }
        }

        /// <summary>
        /// A previous GameState
        /// </summary>
        public GameState Previously
        {
            get
            {
                if (previously == null)
                    previously = new GameState(GetNode("previously"));

                return previously;
            }
        }

        /*
        public GameState Added
        {
            get
            {
                if (added == null)
                    added = new GameState(GetNode("added"));

                return added;
            }
        }
        */

        private String GetNode(string name)
        {
            Newtonsoft.Json.Linq.JToken value;

            if (_ParsedData.TryGetValue(name, out value))
                return value.ToString();
            else
                return "";
        }

        /// <summary>
        /// Returns the json string that generated this GameState instance
        /// </summary>
        /// <returns>Json string</returns>
        public override string ToString()
        {
            return json;
        }
    }
}
