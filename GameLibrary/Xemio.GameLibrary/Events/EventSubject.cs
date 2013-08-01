using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Events
{
    internal class EventSubject : IObservable<IEvent>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the EventSubject class.
        /// </summary>
        public EventSubject()
        {
            this._typeMappings = new Dictionary<Type, List<dynamic>>();
            this._observers = new List<dynamic>();
        }

        #endregion

        #region Fields
        private readonly List<dynamic> _observers;
        private readonly Dictionary<Type, List<dynamic>> _typeMappings; 
        #endregion

        #region Methods
        /// <summary>
        /// Caches the oberservers for the specified event type.
        /// </summary>
        private void CacheOberservers<T>() where T : IEvent
        {
            Type type = typeof(T);

            this._typeMappings.Add(type, new List<dynamic>(
                this._observers.Where(observer => this.IsObserver<T>(observer))));
        }
        /// <summary>
        /// Gets the observers.
        /// </summary>
        private IEnumerable<dynamic> GetObservers<T>() where T : IEvent
        {
            Type type = typeof(T);

            if (!this._typeMappings.ContainsKey(type))
            {
                this.CacheOberservers<T>();
            }

            return this._typeMappings[type];
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
        /// Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        internal void Remove<TEvent>(IObserver<TEvent> observer)
        {
            this._observers.Remove(observer);
        }
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="e">The e.</param>
        public void Publish<TEvent>(TEvent e) where TEvent : IEvent
        {
            try
            {
                foreach (IObserver<TEvent> observer in this.GetObservers<TEvent>())
                {
                    observer.OnNext(e);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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

            return new EventDisposer(this, observer);
        }
        #endregion
    }
}
