using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes.AbilitiesProvider
{
    /// <summary>
    /// Class representing hero ability details.
    /// </summary>
    public class AbilityDetails : Node, IEnumerable<Ability>
    {
        private NodeList<Ability> _abilities = new NodeList<Ability>();

        /// <summary>
        /// The number of abilities.
        /// </summary>
        public int Count { get { return _abilities.Count; } }

        private Regex _ability_regex = new Regex(@"ability(\d+)");

        internal AbilityDetails(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingObjects(parsed_data, _ability_regex, (Match match, JObject obj) =>
            {
                _abilities.Add(new Ability(obj));
            });
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
                if (index < 0 || index > _abilities.Count - 1)
                {
                    return new Ability();
                }

                return _abilities[index];
            }
        }

        /// <summary>
        /// Gets the IEnumerable of abilities.
        /// </summary>
        public IEnumerator<Ability> GetEnumerator()
        {
            return _abilities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _abilities.GetEnumerator();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Abilities: {_abilities}" +
                $"]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }

            return obj is AbilityDetails other &&
                _abilities.Equals(other._abilities);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 356743140;
            hashCode = hashCode * -394407688 + _abilities.GetHashCode();
            return hashCode;
        }
    }
}
