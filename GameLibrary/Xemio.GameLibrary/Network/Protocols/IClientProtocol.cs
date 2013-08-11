using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IClientProtocol : IPackageSender
    {
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        void Connect(string ip, int port);
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Receives a package.
        /// </summary>
        Package Receive();
        /// <summary>
        /// Sets the client.
        /// </summary>
        Client Client { set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IClientProtocol"/> is connected.
        /// </summary>
        bool Connected { get; }
    }
}
