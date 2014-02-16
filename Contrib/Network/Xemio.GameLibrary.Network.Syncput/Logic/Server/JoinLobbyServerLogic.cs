using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Syncput.Core;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;

namespace Xemio.GameLibrary.Network.Syncput.Logic.Server
{
    using Server = Xemio.GameLibrary.Network.Server;

    public class JoinLobbyServerLogic : ServerLogic<JoinLobbyRequest>
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a join lobby request.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(IServer server, JoinLobbyRequest package, IConnection sender)
        {
            SyncputConnection syncConnection = (SyncputConnection)sender;

            var gameLoop = XGL.Components.Get<GameLoop>();
            var syncput = XGL.Components.Get<Syncput>();

            Player player = syncput.Lobby.CreatePlayer(
                package.PlayerName,
                package.FrameIndex - gameLoop.FrameIndex);
            
            syncConnection.Player = player;
            syncput.Lobby.Add(player);

            server.Send(new PlayerJoinedPackage(syncConnection.Player));
        }
        #endregion
    }
}
