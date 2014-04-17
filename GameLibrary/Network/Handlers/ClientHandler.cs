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
        public virtual void OnLostConnection(Client client)
        {
        }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnReceive(Client client, T package)
        {
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnBeginSend(Client client, T package)
        {
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSent(Client client, T package)
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
        void IClientHandler.OnReceive(Client client, Package package)
        {
            this.OnReceive(client, package as T);
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientHandler.OnSending(Client client, Package package)
        {
            this.OnBeginSend(client, package as T);
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientHandler.OnSent(Client client, Package package)
        {
            this.OnSent(client, package as T);
        }
        #endregion
    }
}
