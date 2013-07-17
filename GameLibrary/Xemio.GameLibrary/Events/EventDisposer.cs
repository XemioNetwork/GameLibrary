using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    internal class EventDisposer : IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDisposer"/> class.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public EventDisposer(EventSubject subject, IObserver<IEvent> observer)
        {
            this._subject = subject;
            this._observer = observer;
        }
        #endregion

        #region Fields
        private EventSubject _subject;
        private readonly IObserver<IEvent> _observer;
        #endregion

        #region IDisposable Member
        /// <summary>
        /// Disposes the subscribed event.
        /// </summary>
        public void Dispose()
        {
            this._subject.Remove(this._observer);
        }
        #endregion
    }
}
