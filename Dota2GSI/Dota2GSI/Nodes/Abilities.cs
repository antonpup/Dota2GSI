using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing hero abilities.
    /// </summary>
    public class Abilities : Node, IEnumerable<Ability>
    {
        private List<Ability> abilities = new List<Ability>();

        /// <summary>
        /// The attributes a hero has to spend on abilities.
        /// </summary>
        public readonly Attributes Attributes = new Attributes();

        /// <summary>
        /// The number of abilities.
        /// </summary>
        public int Count { get { return abilities.Count; } }

        internal Abilities(JObject parsed_data = null) : base(parsed_data)
        {
            if (_ParsedData != null)
            {
                List<string> abilities = _ParsedData.Properties().Select(p => p.Name).ToList();
                foreach (string ability_slot in abilities)
                {
                    var ability = _ParsedData[ability_slot] as JObject;

                    if (ability_slot.Equals("attributes"))
                    {
                        Attributes = new Attributes(ability);
                    }
                    else
                    {
                        this.abilities.Add(new Ability(ability));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the ability at a specified index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The ability object</returns>
        public Ability this[int index]
        {
            get
            {
                if (index < 0 || index > abilities.Count - 1)
                {
                    return new Ability();
                }

                return abilities[index];
            }
        }

        /// <summary>
        /// Gets the IEnumerable of abilities.
        /// </summary>
        public IEnumerator<Ability> GetEnumerator()
        {
            return abilities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return abilities.GetEnumerator();
        }
    }
}
