using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;

namespace Xemio.GameLibrary.Network.Syncput.Logic.Server
{
    using Server = Xemio.GameLibrary.Network.Server;

    public class TurnServerLogic : ServerLogic<TurnRequest>
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a turn.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(IServer server, TurnRequest package, IConnection sender)
        {
            SyncputConnection syncConnection = (SyncputConnection)sender;

            if (syncConnection.Player == null)
                return;

            server.Send(new TurnPackage(syncConnection.Player.PlayerIndex, package));
        }
        #endregion
    }
}
