using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public interface IServerHandler
    {
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        Type PackageType { get; }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="package">The package.</param>
        void OnReceive(ServerChannel sender, Package package);
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        void OnSending(ServerChannel receiver, Package package);
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        void OnSent(ServerChannel receiver, Package package);
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="channel">The connection.</param>
        void OnChannelOpened(ServerChannel channel);
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="channel">The connection.</param>
        void OnChannelClosed(ServerChannel channel);
    }
}
