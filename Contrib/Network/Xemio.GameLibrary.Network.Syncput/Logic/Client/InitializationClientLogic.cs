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

namespace Xemio.GameLibrary.Network.Syncput.Logic.Client
{
    using Client = Xemio.GameLibrary.Network.Client;

    public class InitializationClientLogic : ClientLogic<InitializationPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a initialization package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, InitializationPackage package)
        {
            GameLoop gameLoop = XGL.Components.Get<GameLoop>();

            SyncputClient syncputClient = (SyncputClient)client;
            Syncput sync = XGL.Components.Get<Syncput>();

            sync.SynchedRandom.Seed = package.Seed;
            sync.Lobby = new Lobby()
                             {
                                 MaxPlayers = package.MaxPlayers
                             };

            foreach (Player player in package.Players)
            {
                sync.Lobby.Add(player);
            }

            client.Send(new JoinLobbyRequest(syncputClient.PlayerName, gameLoop.FrameIndex));
        }
        #endregion
    }
}
