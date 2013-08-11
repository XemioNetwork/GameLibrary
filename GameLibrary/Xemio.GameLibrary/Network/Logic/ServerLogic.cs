﻿using System;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Logic
{
    public abstract class ServerLogic<T> : IServerLogic where T : Package
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public virtual void OnReceive(Server server, T package, IConnection sender)
        {
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnBeginSend(Server server, T package, IConnection receiver)
        {
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnSent(Server server, T package, IConnection receiver)
        {
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientLeft(Server server, IConnection connection)
        {
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientJoined(Server server, IConnection connection)
        {
        }
        #endregion

        #region IServerSubscriber Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(Server server, float elapsed)
        {
        }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        void IServerLogic.OnReceive(Server server, Package package, IConnection sender)
        {
            this.OnReceive(server, package as T, sender);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerLogic.OnBeginSend(Server server, Package package, IConnection receiver)
        {
            this.OnBeginSend(server, package as T, receiver);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerLogic.OnSent(Server server, Package package, IConnection receiver)
        {
            this.OnSent(server, package as T, receiver);
        }
        #endregion
    }
}
