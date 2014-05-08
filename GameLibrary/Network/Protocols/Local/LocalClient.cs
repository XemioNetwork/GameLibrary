using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalClient : IClientProtocol
    {
        #region Properties
        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string ChannelName { get; private set; }
        #endregion

        #region Implementation of IClientProtocol
        /// <summary>
        /// Sets the client.
        /// </summary>
        public Client Client { set; get; }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public string Id
        {
            get { return "local"; }
        }
        /// <summary>
        /// Starts the protocol and connects corresponding to the site it is being created.
        /// </summary>
        /// <param name="url">The url.</param>
        public void Open(string url)
        {
            this.ChannelName = url;
            if (string.IsNullOrWhiteSpace(url))
            {
                this.ChannelName = "default";
            }

            this.Connected = true;
        }
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        public void Close()
        {
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return Local.GetChannel(this.ChannelName).ClientQueue.Dequeue();
        }
        #endregion

        #region Implementation of ISender
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected { get; private set; }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            Local.GetChannel(this.ChannelName).Send(package, LocalTarget.Server);
        }
        #endregion
    }
}
