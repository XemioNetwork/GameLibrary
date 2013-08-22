using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Network.Packages
{
    internal class ClientPackageReceiver
    {
        #region Fields
        private readonly Client _client;
        private readonly EventManager _eventManager;
        private readonly IThreadInvoker _threadInvoker;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageReceiver"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientPackageReceiver(Client client)
        {
            this._client = client;
            this._eventManager = XGL.Components.Get<EventManager>();
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Starts to receive the packages.
        /// </summary>
        public void StartReceivingPackages()
        {
            Task.Factory.StartNew(() =>
            {
                this.WaitForProtocolConnect();

                try
                {
                    while (this._client.Active && this._client.Protocol.Connected)
                    {
                        Package package = this._client.Protocol.Receive();
                        if (package != null)
                        {
                            this._threadInvoker.Invoke(() => this._client.OnReceivePackage(package));
                            this._eventManager.Publish(new ReceivedPackageEvent(package));
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._eventManager.Publish(new ExceptionEvent(ex));
                }
            });
        }
        #endregion Methods

        #region Private Methods
        /// <summary>
        /// Awaits the receiver.
        /// </summary>
        private void WaitForProtocolConnect()
        {
            while (!this._client.Protocol.Connected)
            {
            }
        }
        #endregion Private Methods
    }
}
