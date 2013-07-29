using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events.Logging
{
    public class LoggingEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingEvent"/> class.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        public LoggingEvent(LoggingLevel level, string message)
        {
            this.Level = level;
            this.Message = message;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the level.
        /// </summary>
        public LoggingLevel Level { get; private set; }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }
        #endregion

        #region IEvent Member
        /// <summary>
        /// Gets a value indicating whether this <see cref="IEvent"/> is synced.
        /// </summary>
        public virtual bool Synced
        {
            get { return false; }
        }
        #endregion
    }
}
