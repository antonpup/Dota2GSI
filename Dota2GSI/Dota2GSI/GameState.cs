using System;
using Dota2GSI.Nodes;

namespace Dota2GSI
{
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
        /// Initialises a new GameState object from JSON Data
        /// </summary>
        /// <param name="json_data"></param>
        public GameState(string json_data)
        {
            if (json_data.Equals(""))
            {
                json_data = "{}";
            }

            json = json_data;
            _ParsedData = Newtonsoft.Json.Linq.JObject.Parse(json_data);
        }

        public Auth Auth
        {
            get
            {
                if (auth == null)
                    auth = new Auth(GetNode("auth"));

                return auth;
            }
        }

        public Provider Provider
        {
            get
            {
                if (provider == null)
                    provider = new Provider(GetNode("provider"));

                return provider;
            }
        }

        public Map Map
        {
            get
            {
                if (map == null)
                    map = new Map(GetNode("map"));

                return map;
            }
        }

        public Player Player
        {
            get
            {
                if (player == null)
                    player = new Player(GetNode("player"));

                return player;
            }
        }

        public Hero Hero
        {
            get
            {
                if (hero == null)
                    hero = new Hero(GetNode("hero"));

                return hero;
            }
        }

        public Abilities Abilities
        {
            get
            {
                if (abilities == null)
                    abilities = new Abilities(GetNode("abilities"));

                return abilities;
            }
        }

        public Items Items
        {
            get
            {
                if (items == null)
                    items = new Items(GetNode("items"));

                return items;
            }
        }

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

        public override string ToString()
        {
            return json;
        }
    }
}
