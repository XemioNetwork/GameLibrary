using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events
{
    public class ClientJoinedEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientJoinedEvent"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ClientJoinedEvent(IConnection connection)
        {
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IConnection Connection { get; private set; }
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
