using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Network.Exceptions
{
    public class ConnectionClosedException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionClosedException"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ConnectionClosedException(IConnection connection) : base(string.Format("{0} disconnected.", connection.Address))
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
    }
}
