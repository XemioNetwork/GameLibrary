using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalServerChannel : IServerChannelProtocol
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServerChannel"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public LocalServerChannel(LocalServer protocol)
        {
            this.ChannelName = protocol.ChannelName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string ChannelName { get; private set; }
        #endregion

        #region Implementation of IServerConnection
        /// <summary>
        /// Gets the internet address.
        /// </summary>
        public IPAddress Address
        {
            get { return IPAddress.Parse("127.0.0.1"); }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return Local.GetChannel(this.ChannelName).ServerQueue.Dequeue();
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public void Close()
        {
        }
        #endregion

        #region Implementation of ISender
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected
        {
            get { return true; }
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            Local.GetChannel(this.ChannelName).Send(package, LocalTarget.Client);
        }
        #endregion
    }
}
