using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IServerProtocol
    {
        /// <summary>
        /// Hosts at the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        void Host(int port);
        /// <summary>
        /// Gets a value indicating whether this <see cref="IServerProtocol"/> is hosted.
        /// </summary>
        bool Hosted { get; }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        IConnection AcceptConnection();
        /// <summary>
        /// Sets the server.
        /// </summary>
        Server Server { set; }
    }
}
