using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalServerConnection : IServerConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServerConnection"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public LocalServerConnection(LocalServerProtocol protocol)
        {
            this.Bridge = protocol.Bridge;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the bridge.
        /// </summary>
        public string Bridge { get; private set; }
        #endregion

        #region Implementation of IServerConnection
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected
        {
            get { return true; }
        }
        /// <summary>
        /// Gets the internet address.
        /// </summary>
        public IPAddress Address
        {
            get { return IPAddress.Parse("127.0.0.1"); }
        }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return Local.Bridge(this.Bridge).ServerQueue.Dequeue();
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            Local.Bridge(this.Bridge).Send(package, LocalTarget.Client);
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public void Disconnect()
        {
        }
        #endregion
    }
}
