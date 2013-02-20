using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Events
{
    public class EventManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        public EventManager()
        {
            this._subscribers = new Dictionary<Type, ActionCollection<Event>>();
        }
        #endregion

        #region Fields
        private Dictionary<Type, ActionCollection<Event>> _subscribers;
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified event through the event manager.
        /// </summary>
        /// <param name="eventInstance">The event instance.</param>
        public void Send(Event eventInstance)
        {
            Type eventType = eventInstance.GetType();
            if (!this._subscribers.ContainsKey(eventType))
            {
                this._subscribers.Add(eventType, new ActionCollection<Event>());
            }

            ActionCollection<Event> collection = this._subscribers[eventType];
            foreach (Action<Event> action in collection)
            {
                action(eventInstance);
            }
        }
        /// <summary>
        /// Registers the subscriber to the specified event.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        public void Subscribe(Type type, Action<Event> action)
        {
            if (!this._subscribers.ContainsKey(type))
            {
                this._subscribers.Add(type, new ActionCollection<Event>());
            }

            this._subscribers[type].Add(action);
        }
        /// <summary>
        /// Registers the subscriber to the specified event.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Subscribe<T>(Action<T> action) where T : Event
        {
            this.Subscribe(typeof(T), e => action(e as T));
        }
        #endregion
    }
}
