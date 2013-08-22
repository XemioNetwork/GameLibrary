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
    internal class ServerPackageReceiver
    {
        #region Fields
        private readonly Server _server;
        private readonly EventManager _eventManager;
        private readonly IThreadInvoker _threadInvoker;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerPackageReceiver"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public ServerPackageReceiver(Server server)
        {
            this._server = server;

            this._eventManager = XGL.Components.Get<EventManager>();
            this._threadInvoker = XGL.Components.Get<IThreadInvoker>();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Starts to receive packages for the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public void StartReceivingPackages(IConnection connection)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (connection.Connected)
                    {
                        Package package = connection.Receive();
                        if (package != null)
                        {
                            this._threadInvoker.Invoke(() => 
                                this._server.OnReceivePackage(package, connection));
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._eventManager.Publish(new ExceptionEvent(ex));

                    this._server.Connections.Remove(connection);
                    this._server.OnClientLeft(connection);
                }
            });
        }
        #endregion Methods
    }
}
