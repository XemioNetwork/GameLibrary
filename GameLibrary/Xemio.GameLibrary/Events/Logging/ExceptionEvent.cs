using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events.Logging
{
    public class ExceptionEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionEvent"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExceptionEvent(Exception exception)
        {
            this.Exception = exception;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; private set; }
        #endregion

        #region IEvent Member
        /// <summary>
        /// Gets a value indicating whether this <see cref="IEvent"/> is synced.
        /// </summary>
        public bool Synced
        {
            get { return false; }
        }
        #endregion
    }
}
