using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    internal class ActionObserver<T> : IObserver<T> where T : class, IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionObserver&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public ActionObserver(Action<T> action)
        {
            this._action = action;
        }
        #endregion

        #region Fields
        private readonly Action<T> _action;
        #endregion

        #region IObserver<IEvent> Member
        /// <summary>
        /// Called when the observer doesn't send events anymore.
        /// </summary>
        public void OnCompleted()
        {
        }
        /// <summary>
        /// Called when an exception occurs inside the observer.
        /// </summary>
        /// <param name="error">The error.</param>
        public void OnError(Exception error)
        {
        }
        /// <summary>
        /// Called when an event instance arrives.
        /// </summary>
        /// <param name="value">The value.</param>
        public void OnNext(T value)
        {
            this._action(value);
        }
        #endregion
    }
}
