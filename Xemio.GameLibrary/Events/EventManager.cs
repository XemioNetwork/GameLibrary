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
            this._subject = new EventSubject();
        }
        #endregion

        #region Fields
        private EventSubject _subject;
        #endregion

        #region Methods
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">The event.</param>
        public void Publish<T>(T e) where T : IEvent
        {
            this._subject.Publish(e);
        }
        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Subscribe(IObserver<IEvent> observer)
        {
            this._subject.Subscribe(observer);
        }
        /// <summary>
        /// Subscribes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Subscribe<T>(Action<T> action) where T : class, IEvent
        {
            this._subject.Subscribe(new ActionObserver<T>(action));
        }
        /// <summary>
        /// Observes this instance.
        /// </summary>
        public IObservable<IEvent> Observe()
        {
            return this._subject;
        }
        #endregion
    }
}
