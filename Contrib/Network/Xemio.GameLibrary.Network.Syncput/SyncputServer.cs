using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Syncput.Logic.Server;
using Xemio.GameLibrary.Network.Syncput.Packages;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class SyncputServer : Server
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncputServer"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public SyncputServer(IServerProtocol protocol) : base(protocol)
        {
            this.Subscribe(new JoinLobbyServerLogic());
            this.Subscribe(new MessageServerLogic());
            this.Subscribe(new TurnServerLogic());
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Accepts the connection.
        /// </summary>
        protected override IConnection AcceptConnection()
        {
            return new SyncputConnection(base.AcceptConnection());
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected override void OnClientJoined(IConnection connection)
        {
            Syncput sync = XGL.Components.Get<Syncput>();

            InitializationPackage package = new InitializationPackage(
                sync.SynchedRandom.Seed, 
                sync.Lobby.MaxPlayers,
                sync.Lobby.ToList());

            base.OnClientJoined(connection);
            this.Send(package, connection);
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected override void OnClientLeft(IConnection connection)
        {
            Syncput sync = XGL.Components.Get<Syncput>();
            SyncputConnection syncConnection = (SyncputConnection)connection;

            if (syncConnection.Player != null)
            {
                sync.Lobby.Remove(syncConnection.Player.Id);
                this.Send(new PlayerLeftPackage(syncConnection.Player));
            }

            base.OnClientLeft(connection);
        }
        #endregion
    }
}
