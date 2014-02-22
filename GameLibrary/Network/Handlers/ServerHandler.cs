using System;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public abstract class ServerHandler<T> : IServerHandler where T : Package
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public virtual void OnReceive(IServer server, T package, IServerConnection sender)
        {
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnBeginSend(IServer server, T package, IServerConnection receiver)
        {
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public virtual void OnSent(IServer server, T package, IServerConnection receiver)
        {
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientLeft(IServer server, IServerConnection connection)
        {
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public virtual void OnClientJoined(IServer server, IServerConnection connection)
        {
        }
        #endregion

        #region IClientHandler Member
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        public Type PackageType
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        void IServerHandler.OnReceive(IServer server, Package package, IServerConnection sender)
        {
            this.OnReceive(server, package as T, sender);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerHandler.OnSending(IServer server, Package package, IServerConnection receiver)
        {
            this.OnBeginSend(server, package as T, receiver);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void IServerHandler.OnSent(IServer server, Package package, IServerConnection receiver)
        {
            this.OnSent(server, package as T, receiver);
        }
        #endregion
    }
}
