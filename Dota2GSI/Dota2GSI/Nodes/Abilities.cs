using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dota2GSI.Nodes
{
    public class Abilities : Node, IEnumerable<Ability>
    {
        private List<Ability> abilities = new List<Ability>();
        public readonly Attributes Attributes;

        private string json;

        public int Count { get { return abilities.Count; } }

        internal Abilities(string json_data) : base(json_data)
        {
            json = json_data;

            List<string> abilities = _ParsedData.Properties().Select(p => p.Name).ToList();
            foreach (string ability_slot in abilities)
            {
                if (ability_slot.Equals("attributes"))
                    Attributes = new Attributes(_ParsedData[ability_slot].ToString());
                else
                    this.abilities.Add(new Ability(_ParsedData[ability_slot].ToString()));
            }
        }

        /// <summary>
        /// Gets the ability in the selected index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Ability this[int index]
        {
            get
            {
                if (index > abilities.Count - 1)
                    return new Ability("");

                return abilities[index];
            }
        }

        public IEnumerator<Ability> GetEnumerator()
        {
            return this.abilities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.abilities.GetEnumerator();
        }

        public override string ToString()
        {
            return json;
        }

        
    }
}
