using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    internal class ActionObserver<T> : IObserver<IEvent> where T : class
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
        public void OnCompleted()
        {
        }
        public void OnError(Exception error)
        {
        }
        public void OnNext(IEvent value)
        {
            this._action(value as T);
        }
        #endregion
    }
}
