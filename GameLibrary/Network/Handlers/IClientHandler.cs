using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public interface IClientHandler
    {
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        Type PackageType { get; }
        /// <summary>
        /// Called when the client connection got disconnected.
        /// </summary>
        /// <param name="client">The client.</param>
        void OnLostConnection(Client client);
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnReceive(Client client, Package package);
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnSending(Client client, Package package);
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnSent(Client client, Package package);
    }
}
