using System;
using System.Collections.Generic;
using EventBus.Singleton;

namespace EventBus
{
    /// <summary>
    /// This class is controlling all events on the project with channels. 
    /// Channels are basicly groups of events for recognizing process.  
    /// Other words, channels just representing what kind of events triggering.
    /// This system is working with subscribe-publish approach.
    /// </summary>
    internal class EventBus : SingletonBase<EventBus>
    {
        /// <summary>
        /// Channels can be expanded if necessary.
        /// </summary>
        public enum EventBusChannelKeys
        {
            GENERAL_CHANNEL = 0,            
            USER_INPUT_CHANNEL = 1,
            UI_CHANNEL = 2
        }

        private readonly Dictionary<EventBusChannelKeys, Dictionary<Type, List<Action<object>>>> _channels;

        public EventBus() {
            _channels = new Dictionary<EventBusChannelKeys, Dictionary<Type, List<Action<object>>>>();
        }

        /// <summary>
        /// This function is subscribing functions to channels.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="action"></param>
        public void Subscribe<T>(EventBusChannelKeys channel, Action<T> action)
        {
            if (!_channels.ContainsKey(channel))
            {
                _channels[channel] = new Dictionary<Type, List<Action<object>>>();
            }

            Type type = typeof(T);
            if (!_channels[channel].ContainsKey(type))
            {
                _channels[channel][type] = new List<Action<object>>();
            }

            _channels[channel][type].Add(e => action((T)e));
        }

        /// <summary>
        /// This function is triggering event with channel information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="eventMessage"></param>
        public void Publish<T>(EventBusChannelKeys channel, T eventMessage)
        {
            if (_channels.ContainsKey(channel))
            {
                Type type = typeof(T);
                if (_channels[channel].ContainsKey(type))
                {
                    foreach (var action in _channels[channel][type])
                    {
                        action.Invoke(eventMessage); // ?? action(eventMessage);
                    }
                }
            }
        }

        public void Unsubscribe<T>(EventBusChannelKeys channel, Action<T> action)
        {
            if (_channels.ContainsKey(channel))
            {
                Type type = typeof(T);
                if (_channels[channel].ContainsKey(type))
                {
                    _channels[channel][type].RemoveAll(act => act.Equals(action));
                    if (_channels[channel][type].Count == 0)
                    {
                        _channels[channel].Remove(type);
                    }
                }
                if (_channels[channel].Count == 0)
                {
                    _channels.Remove(channel);
                }
            }
        }
    }
}
