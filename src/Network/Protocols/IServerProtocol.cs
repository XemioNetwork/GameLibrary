using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IServerProtocol : IProtocol
    {
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        IServerConnection AcceptConnection();
        /// <summary>
        /// Sets the server.
        /// </summary>
        Server Server { set; }
    }
}
