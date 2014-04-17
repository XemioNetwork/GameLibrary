using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Handlers.Attributes;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public class ReflectedClientHandler : ReflectedHandler<IClientHandlerAttribute>, IClientHandler
    {
        #region Implementation of IClientHandler
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        public Type PackageType
        {
            get { return typeof(Package); }
        }
        /// <summary>
        /// Called when the client connection got disconnected.
        /// </summary>
        /// <param name="client">The client.</param>
        public void OnLostConnection(Client client)
        {
            this.Invoke<OnDisconnectedAttribute>(client);
        }
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public void OnReceive(Client client, Package package)
        {
            this.Invoke<OnReceiveAttribute>(method => this.Matches(method, package), client, package);
        }
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public void OnSending(Client client, Package package)
        {
            this.Invoke<OnSendingAttribute>(method => this.Matches(method, package), client, package);
        }
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public void OnSent(Client client, Package package)
        {
            this.Invoke<OnSendingAttribute>(method => this.Matches(method, package), client, package);
        }
        #endregion
    }
}
