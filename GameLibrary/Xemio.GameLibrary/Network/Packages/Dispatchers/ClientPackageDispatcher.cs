using System;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Exceptions;

namespace Xemio.GameLibrary.Network.Packages.Dispatchers
{
    internal class ClientPackageDispatcher : Worker
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Client _client;
        private readonly IThreadInvoker _threadInvoker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageDispatcher"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientPackageDispatcher(Client client)
        {
            this._client = client;
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion
        
        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
            logger.Info("Starting client package dispatcher.");

            try
            {
                while (this._client.Connected)
                {
                    Package package = this._client.Protocol.Receive();
                    if (package != null)
                    {
                        logger.Trace("Received {0}.", package.GetType().Name);
                        this._threadInvoker.Invoke(() => this._client.OnReceivePackage(package));
                    }
                    else
                    {
                        logger.Warn("Received invalid package.");
                    }
                }
            }
            catch (ClientLostConnectionException ex)
            {
                logger.Info("Lost connection to the server.");
            }
            catch (Exception ex)
            {
                logger.Error("Error while receiving package from server.", ex);
            }
        }
        #endregion
    }
}
