using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Network.Handlers;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network
{
    [AbstractComponent]
    public interface IClient : IComponent
    {
        /// <summary>
        /// Gets the latency.
        /// </summary>
        float Latency { get; set; }
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        IClientProtocol Protocol { get; }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        void Send(Package package);
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Subscribe(IClientHandler subscriber);
        /// <summary>
        /// Unsubscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Unsubscribe(IClientHandler subscriber);
        /// <summary>
        /// Stops the client.
        /// </summary>
        void Close();
    }
}
