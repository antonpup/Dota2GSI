using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing events information.
    /// </summary>
    public class Events : IEnumerable<Event>
    {
        private List<Event> _events = new List<Event>();

        /// <summary>
        /// The number of events.
        /// </summary>
        public int Count { get { return _events.Count; } }

        internal Events(JObject parsed_data = null) : base()
        {
            if (parsed_data != null)
            {
                if (parsed_data.Type == JTokenType.Array)
                {
                    foreach(JToken element in parsed_data.Children())
                    {
                        if (element.Type == JTokenType.Object)
                        {
                            _events.Add(new Event(element as JObject));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the event at a specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The event.</returns>
        public Event this[int index]
        {
            get
            {
                if (index < 0 || index > _events.Count - 1)
                {
                    return new Event();
                }

                return _events[index];
            }
        }

        /// <summary>
        /// Gets the IEnumerable of events.
        /// </summary>
        public IEnumerator<Event> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _events.GetEnumerator();
        }
    }
}
