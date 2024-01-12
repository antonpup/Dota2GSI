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
        /// Gets events for a specific team.<br/>
        /// </summary>
        /// <param name="team">The team.</param>
        /// <returns>List of events.</returns>
        public List<Event> GetForTeam(PlayerTeam team)
        {
            List<Event> found_events = new List<Event>();

            foreach (var evt in _events)
            {
                if (evt.Team == team)
                {
                    found_events.Add(evt);
                }
            }

            return found_events;
        }

        /// <summary>
        /// Gets events for a specific player.<br/>
        /// </summary>
        /// <param name="player_id">The player id to match.</param>
        /// <returns>List of events.</returns>
        public List<Event> GetForPlayer(int player_id)
        {
            List<Event> found_events = new List<Event>();

            foreach (var evt in _events)
            {
                if (evt.PlayerID == player_id || evt.KillerPlayerID == player_id || evt.TipReceiverPlayerID == player_id)
                {
                    found_events.Add(evt);
                }
            }

            return found_events;
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[" +
                $"Events: {_events}" +
                $"]";
        }
    }
}
