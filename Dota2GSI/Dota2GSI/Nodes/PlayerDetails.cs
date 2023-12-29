using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// A class representing player details.
    /// </summary>
    public class PlayerDetails
    {
        private JObject token;
        private string name;
        private Player player;
        private Hero hero;
        private Abilities abilities;
        private Items items;

        /// <summary>
        /// Information about the player.
        /// </summary>
        public Player Player
        {
            get
            {
                if (player == null)
                {
                    player = new Player(GetNode("player." + name));
                }

                return player;
            }
        }

        /// <summary>
        /// Information about the player's hero.
        /// </summary>
        public Hero Hero
        {
            get
            {
                if (hero == null)
                {
                    hero = new Hero(GetNode("hero." + name));
                }

                return hero;
            }
        }

        /// <summary>
        /// Information about the player's hero abilities.
        /// </summary>
        public Abilities Abilities
        {
            get
            {
                if (abilities == null)
                {
                    abilities = new Abilities(GetNode("abilities." + name));
                }

                return abilities;
            }
        }

        /// <summary>
        /// Information about the player's hero items.
        /// </summary>
        public Items Items
        {
            get
            {
                if (items == null)
                {
                    items = new Items(GetNode("items." + name));
                }

                return items;
            }
        }

        internal PlayerDetails(JObject json, string name)
        {
            token = json;
            this.name = name;
        }

        private string GetNode(string name)
        {
            JToken value;

            if ((value = token.SelectToken(name)) != null)
            {
                return value.ToString();
            }

            return "";
        }
    }
}
