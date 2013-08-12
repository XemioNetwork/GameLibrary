using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Logic
{
    public interface IClientLogic
    {
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        Type PackageType { get; }
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
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(IClient client, float elapsed);
    }
}
