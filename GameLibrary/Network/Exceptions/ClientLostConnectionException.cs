using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Network.Exceptions
{
    public class ClientLostConnectionException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLostConnectionException"/> class.
        /// </summary>
        public ClientLostConnectionException() : base("Disconnected from server.")
        {
        }
        #endregion
    }
}
