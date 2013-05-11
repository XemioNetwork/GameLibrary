using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.Testing.EventSystem
{
    public class TestEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestEvent"/> class.
        /// </summary>
        public TestEvent(string message)
        {
            this.Synced = false;
            this.Message = message;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        #endregion

        #region Implementation of IEvent
        /// <summary>
        /// Gets a value indicating whether this <see cref="IEvent"/> is synced.
        /// </summary>
        public bool Synced { get; private set; }
        #endregion
    }
}
