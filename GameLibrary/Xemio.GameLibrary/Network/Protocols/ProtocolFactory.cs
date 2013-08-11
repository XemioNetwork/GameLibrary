using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Protocols.Tcp;

namespace Xemio.GameLibrary.Network.Protocols
{
    public static class ProtocolFactory
    {
        #region Methods
        /// <summary>
        /// Creates a new client for the specified protocol.
        /// </summary>
        /// <typeparam name="T">The protocol type.</typeparam>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public static Client CreateClientFor<T>(string ip, int port) where T : IClientProtocol, new()
        {
            T protocol = new T();
            protocol.Connect(ip, port);

            return new Client(protocol);
        }
        /// <summary>
        /// Creates a new server for the specified protocol.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public static Server CreateServerFor<T>(int port) where T : IServerProtocol, new()
        {
            T protocol = new T();
            protocol.Host(port);

            return new Server(protocol);
        }
        #endregion
    }
}
