using System;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ClientPackageProcessor : Worker
    {
        #region Fields
        private readonly Client _client;
        private readonly EventManager _eventManager;
        private readonly IThreadInvoker _threadInvoker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientPackageProcessor"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientPackageProcessor(Client client)
        {
            this._client = client;

            this._eventManager = XGL.Components.Get<EventManager>();
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion
        
        #region Overrides of Worker
        /// <summary>
        /// Executes tasks if the start method got called.
        /// </summary>
        protected override void Run()
        {
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
        }
        #endregion
    }
}
