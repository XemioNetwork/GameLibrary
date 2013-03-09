﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IServerProtocol
    {
        /// <summary>
        /// Sends the specified package to the specified receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void Send(Package package, IConnection receiver);
        /// <summary>
        /// Receives a package.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        Package Receive(IConnection connection);
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