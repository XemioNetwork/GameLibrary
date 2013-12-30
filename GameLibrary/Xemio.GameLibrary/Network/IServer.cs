﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network
{
    [AbstractComponent]
    public interface IServer : ISender, IComponent
    {
        /// <summary>
        /// Gets the connections.
        /// </summary>
        IList<IConnection> Connections { get; }
        /// <summary>
        /// Accepts a connection.
        /// </summary>
        IConnection AcceptConnection();
        /// <summary>
        /// Sends the specified package to the receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void Send(Package package, IConnection receiver);
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Subscribe(IServerHandler subscriber);
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Unsubscribe(IServerHandler subscriber);
        /// <summary>
        /// Stops the server.
        /// </summary>
        void Close();
        /// <summary>
        /// Called when the server received a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        void OnReceivePackage(Package package, IConnection connection);
        /// <summary>
        /// Called when server is sending a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        void OnBeginSendPackage(Package package, IConnection connection);
        /// <summary>
        /// Called when server sent a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        void OnSentPackage(Package package, IConnection connection);
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void OnClientJoined(IConnection connection);
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void OnClientLeft(IConnection connection);
    }
}
