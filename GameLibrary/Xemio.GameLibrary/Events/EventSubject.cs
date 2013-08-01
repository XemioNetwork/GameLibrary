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
            this._observers = new List<dynamic>();
            this._observerTypeMappings = new Dictionary<Pair<Type, Type>, bool>();
        }
        #endregion

        #region Fields
        private readonly List<dynamic> _observers;
        private readonly Dictionary<Pair<Type, Type>, bool> _observerTypeMappings; 
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified observer is observing the specified type.
        /// </summary>
        /// <param name="observer">The observer.</param>
        private bool IsObserver<TEvent>(dynamic observer)
        {
            Type type = observer.GetType();
            Type targetType = typeof(TEvent);

            Pair<Type, Type> pair = new Pair<Type, Type>(type, targetType);

            if (!this._observerTypeMappings.ContainsKey(pair))
            {
                Type genericType = type.GetGenericArguments().First();
                this._observerTypeMappings.Add(pair, genericType.IsAssignableFrom(targetType));
            }

            return this._observerTypeMappings[pair];
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
        public void Publish<TEvent>(TEvent e)
        {
            try
            {
                IEnumerable<IObserver<TEvent>> observers = this._observers
                    .Where(observer => this.IsObserver<TEvent>(observer))
                    .Select(observer => observer as IObserver<TEvent>);

                foreach (IObserver<TEvent> observer in observers)
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
            return new EventDisposer(this, observer);
        }
        #endregion
    }
}
