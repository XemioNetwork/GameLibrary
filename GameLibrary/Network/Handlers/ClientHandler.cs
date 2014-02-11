using System;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public abstract class ClientHandler<T> : IClientHandler where T : Package
    {
        #region Methods
        /// <summary>
        /// Called when the client connection got disconnected.
        /// </summary>
        /// <param name="client">The client.</param>
        public virtual void OnDisconnected(IClient client)
        {
        }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnReceive(IClient client, T package)
        {
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnBeginSend(IClient client, T package)
        {
        }
        /// <summary>
        /// Posts the send.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSent(IClient client, T package)
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
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientHandler.OnReceive(IClient client, Package package)
        {
            this.OnReceive(client, package as T);
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientHandler.OnBeginSend(IClient client, Package package)
        {
            this.OnBeginSend(client, package as T);
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientHandler.OnSent(IClient client, Package package)
        {
            this.OnSent(client, package as T);
        }
        #endregion
    }
}
