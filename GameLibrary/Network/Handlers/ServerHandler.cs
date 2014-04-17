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
        /// <param name="sender">The sender.</param>
        /// <param name="package">The package.</param>
        public virtual void OnReceive(ServerChannel sender, T package)
        {
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSending(ServerChannel receiver, T package)
        {
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSent(ServerChannel receiver, T package)
        {
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="channel">The connection.</param>
        public virtual void OnChannelClosed(ServerChannel channel)
        {
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public virtual void OnChannelOpened(ServerChannel channel)
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
        /// <param name="sender">The sender.</param>
        /// <param name="package">The package.</param>
        void IServerHandler.OnReceive(ServerChannel sender, Package package)
        {
            this.OnReceive(sender, package as T);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        void IServerHandler.OnSending(ServerChannel receiver, Package package)
        {
            this.OnSending(receiver, package as T);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        void IServerHandler.OnSent(ServerChannel receiver, Package package)
        {
            this.OnSent(receiver, package as T);
        }
        #endregion
    }
}
