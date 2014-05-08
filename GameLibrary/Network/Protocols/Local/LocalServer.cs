using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalServer : IServerProtocol
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServer"/> class.
        /// </summary>
        public LocalServer()
        {
            this._lock = new AutoResetEvent(false);
            this._lock.Set();
        }
        #endregion

        #region Fields
        private readonly AutoResetEvent _lock;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string ChannelName { get; private set; }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Sets the server.
        /// </summary>
        public Server Server { set; private get; }
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
        }
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        public void Close()
        {
        }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        public IServerChannelProtocol AcceptChannel()
        {
            this._lock.WaitOne();
            return new LocalServerChannel(this);
        }
        #endregion
    }
}
