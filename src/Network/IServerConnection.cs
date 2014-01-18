﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Nested;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;
using System.Net;

namespace Xemio.GameLibrary.Network
{
    public interface IServerConnection : ISender
    {
        /// <summary>
        /// Gets the internet address.
        /// </summary>
        IPAddress Address { get; }
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
    }

    public static class ConnectionExtensions
    {
        #region Methods
        /// <summary>
        /// Resolves the specified connection and removes the INestedConnection wrapper.
        /// </summary>
        /// <typeparam name="T">The connection type</typeparam>
        /// <param name="connection">The connection.</param>
        public static T Resolve<T>(this IServerConnection connection)
        {
            IServerConnection current = connection;
            while (current is INestedServerConnection && !(current is T))
            {
                current = ((INestedServerConnection)current).Connection;
            }

            return (T)current;
        }
        #endregion
    }
}