using Dota2GSI.EventMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Dota2GSI
{
    public class EventDispatcher<T> where T : BaseEvent
    {
        /// <summary>
        /// Delegate for handing game events.
        /// </summary>
        /// <param name="game_event">The new game event.</param>
        public delegate void GameEventHandler(T game_event);

        /// <summary>
        /// Event for handing game events.
        /// </summary>
        public event GameEventHandler GameEvent = delegate { };

        private readonly object subscriptions_lock = new object();

        private Dictionary<Type, HashSet<Action<T>>> subscriptions = new Dictionary<Type, HashSet<Action<T>>>();
        private Dictionary<Type, HashSet<Func<T, T>>> pre_processors = new Dictionary<Type, HashSet<Func<T, T>>>();

        public EventDispatcher()
        {
            Subscribe<T>(RaiseOnGameEventHandler);
        }

        ~EventDispatcher()
        {
            Unsubscribe<T>(RaiseOnGameEventHandler);
        }

        private void RaiseOnGameEventHandler(T game_event)
        {
            foreach (Delegate d in GameEvent.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                {
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, new object[] { game_event });
                }
                else
                {
                    d.DynamicInvoke(game_event);
                }
            }
        }

        public void RegisterPreProcessor<MessageType>(Func<T, T> callback) where MessageType : T
        {

            var event_type = typeof(MessageType);

            lock (subscriptions_lock)
            {
                if (!pre_processors.ContainsKey(event_type))
                {
                    pre_processors.Add(event_type, new HashSet<Func<T, T>>());
                }

                pre_processors[event_type].Add(callback);
            }
        }

        public void UnregisterPreProcessor<MessageType>(Func<T, T> callback) where MessageType : T
        {

            var event_type = typeof(MessageType);

            lock (subscriptions_lock)
            {
                if (!subscriptions.ContainsKey(event_type))
                {
                    return;
                }

                pre_processors[event_type].Remove(callback);
            }
        }


        public void Subscribe<MessageType>(Action<T> callback) where MessageType : T
        {
            var event_type = typeof(MessageType);

            lock (subscriptions_lock)
            {
                if (!subscriptions.ContainsKey(event_type))
                {
                    subscriptions.Add(event_type, new HashSet<Action<T>>());
                }

                subscriptions[event_type].Add(callback);
            }
        }

        public void Unsubscribe<MessageType>(Action<T> callback) where MessageType : T
        {
            var event_type = typeof(MessageType);

            lock (subscriptions_lock)
            {
                if (!subscriptions.ContainsKey(event_type))
                {
                    return;
                }

                subscriptions[event_type].Remove(callback);
            }
        }

        public void Broadcast<MessageType>(MessageType message) where MessageType : T
        {
            var event_type = typeof(MessageType);
            T msg = message;

            lock (subscriptions_lock)
            {
                if (subscriptions.ContainsKey(event_type))
                {
                    // Run pre-processors first
                    if (pre_processors.ContainsKey(event_type))
                    {
                        foreach (var pre_processor in pre_processors[event_type])
                        {
                            msg = pre_processor(msg);

                            if (msg == null)
                            {
                                // The message was handled.
                                return;
                            }
                        }
                    }

                    foreach (var callback in subscriptions[event_type])
                    {
                        callback.Invoke(msg);
                    }
                }

                if (event_type != typeof(T))
                {
                    Broadcast(msg);
                }
            }
        }
    }
}
