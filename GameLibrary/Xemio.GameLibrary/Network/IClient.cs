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
    public interface IClient : ISender, IComponent
    {
        /// <summary>
        /// Gets the latency.
        /// </summary>
        float Latency { get; set; }
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
        /// <summary>
        /// Called when the client gets disconnected.
        /// </summary>
        void OnDisconnected();
        /// <summary>
        /// Calls the client handlers when the specified package was received.
        /// </summary>
        /// <param name="package">The package.</param>
        void OnReceivePackage(Package package);
        /// <summary>
        /// Calls the client handlers when the specified package is going to be send.
        /// </summary>
        /// <param name="package">The package.</param>
        void OnBeginSendPackage(Package package);
        /// <summary>
        /// Calls the client handlers when the specified package is sent.
        /// </summary>
        /// <param name="package">The package.</param>
        void OnSentPackage(Package package);
    }
}
