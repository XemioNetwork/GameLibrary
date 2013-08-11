using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;
using System.Net;

namespace Xemio.GameLibrary.Network
{
    public interface IConnection : IPackageSender
    {
        /// <summary>
        /// Gets the IP.
        /// </summary>
        IPAddress IP { get; }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        float Latency { get; set; }        
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Receives a package.
        /// </summary>
        Package Receive();
        /// <summary>
        /// Gets a value indicating whether this <see cref="IClientProtocol"/> is connected.
        /// </summary>
        bool Connected { get; }
    }
    public static class ConnectionExtensions
    {
        #region Methods
        /// <summary>
        /// Resolves the specified connection and removes the INestedConnection wrapper.
        /// </summary>
        /// <typeparam name="T">The connection type</typeparam>
        /// <param name="connection">The connection.</param>
        public static T Resolve<T>(this IConnection connection)
        {
            IConnection current = connection;
            while (current is INestedConnection && !(current is T))
            {
                current = (current as INestedConnection).Connection;
            }

            return (T)current;
        }
        #endregion
    }
}
