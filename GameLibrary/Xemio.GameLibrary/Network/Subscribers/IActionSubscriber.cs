using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Subscribers
{
    public interface IActionSubscriber
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        void OnReceive(Server server, Package package, IConnection sender);
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void OnBeginSend(Server server, Package package, IConnection receiver);
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        void OnSent(Server server, Package package, IConnection receiver);
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(float elapsed);
    }
}
