using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NLog;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalServerProtocol : IServerProtocol
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalServerProtocol"/> class.
        /// </summary>
        public LocalServerProtocol()
        {
            this._deadLock = new AutoResetEvent(false);
            this._deadLock.Set();
        }
        #endregion

        #region Fields
        private readonly AutoResetEvent _deadLock;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the bridge.
        /// </summary>
        public string Bridge { get; private set; }
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
            this.Bridge = url;
            if (string.IsNullOrWhiteSpace(url))
            {
                this.Bridge = "default";
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
        public IServerConnection AcceptConnection()
        {
            this._deadLock.WaitOne();
            return new LocalServerConnection(this);
        }
        #endregion
    }
}
