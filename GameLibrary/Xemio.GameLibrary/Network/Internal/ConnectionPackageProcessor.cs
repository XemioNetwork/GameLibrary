using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Internal
{
    internal class ConnectionPackageProcessor : Worker
    {
        #region Fields
        private readonly Server _server;
        private readonly IConnection _connection;

        private readonly EventManager _eventManager;
        private readonly IThreadInvoker _threadInvoker;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionPackageProcessor" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public ConnectionPackageProcessor(Server server, IConnection connection)
        {
            this._server = server;
            this._connection = connection;

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
                while (this._connection.Connected)
                {
                    Package package = this._connection.Receive();
                    if (package != null)
                    {
                        this._threadInvoker.Invoke(() => this._server.OnReceivePackage(package, this._connection));
                    }
                }
            }
            catch (Exception ex)
            {
                this._eventManager.Publish(new ExceptionEvent(ex));

                this._server.Connections.Remove(this._connection);
                this._server.OnClientLeft(this._connection);
            }
        }
        #endregion
    }
}
