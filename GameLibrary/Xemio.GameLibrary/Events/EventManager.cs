using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Events
{
    public class EventManager : IComponent, IObservable<IEvent>
    {
        #region Fields
        private readonly List<dynamic> _observers = new List<dynamic>();
        private readonly Dictionary<Type, List<dynamic>> _typeMappings = new Dictionary<Type, List<dynamic>>(); 
        #endregion

        #region Private Methods
        /// <summary>
        /// Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        private void Remove<TEvent>(IObserver<TEvent> observer)
        {
            this._observers.Remove(observer);
            this._typeMappings.Remove(typeof(TEvent));
        }
        /// <summary>
        /// Determines whether the specified observer is observing the specified type.
        /// </summary>
        /// <param name="observer">The observer.</param>
        private bool IsObserver<TEvent>(dynamic observer)
        {
            Type type = observer.GetType();
            Type targetType = typeof(TEvent);
            Type genericType = type.GetGenericArguments().First();

            return genericType.IsAssignableFrom(targetType);
        }
        /// <summary>
        /// Gets the observers.
        /// </summary>
        private IEnumerable<dynamic> GetObservers<T>() where T : IEvent
        {
            Type type = typeof(T);

            if (!this._typeMappings.ContainsKey(type))
            {
                this._typeMappings.Add(type, new List<dynamic>(
                    this._observers.Where(observer => this.IsObserver<T>(observer))));
            }

            return this._typeMappings[type];
        }
        #endregion Private Methods

        #region Methods
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="e">The event.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            foreach (IObserver<TEvent> observer in this.GetObservers<TEvent>())
            {
                observer.OnNext(e);
            }
        }
        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public IDisposable Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            return this.Subscribe(new ActionObserver<T>(action));
        }
        #endregion

        #region IObservable<IEvent> Member
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public IDisposable Subscribe(IObserver<IEvent> observer)
        {
            this._observers.Add(observer);
            this._typeMappings.Clear();

            return new ActionDisposable(() => this.Remove(observer));
        }
        #endregion
    }
}
