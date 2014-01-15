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
        void OnDisconnected(IClient client);
        /// <summary>
        /// Called when a package arrives.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnReceive(IClient client, Package package);
        /// <summary>
        /// Called when a package is being sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnBeginSend(IClient client, Package package);
        /// <summary>
        /// Called when a package was sent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        void OnSent(IClient client, Package package);
    }
}
