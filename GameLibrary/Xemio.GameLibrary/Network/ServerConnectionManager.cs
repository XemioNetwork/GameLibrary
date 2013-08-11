using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network
{
    internal class ServerConnectionManager
    {
        #region Fields
        private readonly Server _server;
        private readonly ServerPackageReceiver _receiver;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionManager"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public ServerConnectionManager(Server server)
        {
            this._server = server;
            this._receiver = new ServerPackageReceiver(server);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Awaits the protocol to host its connection.
        /// </summary>
        private void AwaitHosting()
        {
            while (!this._server.Protocol.Hosted)
            {
            }
        }
        /// <summary>
        /// Starts to accept the accepting connections.
        /// </summary>
        public void StartAcceptingConnections()
        {
            Task.Factory.StartNew(() =>
            {
                this.AwaitHosting();

                while (this._server.Active)
                {
                    IConnection connection = this._server.AcceptConnection();

                    this._server.Connections.Add(connection);
                    this._receiver.StartReceivingPackages(connection);

                    this._server.OnClientJoined(connection);
                }
            });
        }
        #endregion Methods
    }
}
