using System;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Logic
{
    public abstract class ClientLogic<T> : IClientLogic where T : Package
    {
        #region Methods
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
        /// Posts the send.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public virtual void OnSent(Client client, T package)
        {
        }
        #endregion

        #region IPackageSubscriber Member
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
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientLogic.OnReceive(Client client, Package package)
        {
            this.OnReceive(client, package as T);
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientLogic.OnBeginSend(Client client, Package package)
        {
            this.OnBeginSend(client, package as T);
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void IClientLogic.OnSent(Client client, Package package)
        {
            this.OnSent(client, package as T);
        }
        #endregion
    }
}
