using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Syncput.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Logic.Client
{
    using Client = Xemio.GameLibrary.Network.Client;

    public class PlayerLeftClientLogic : ClientLogic<PlayerLeftPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a player left package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, PlayerLeftPackage package)
        {
            var sync = XGL.Components.Get<Syncput>();
            sync.Lobby.Remove(package.Player.Id);
        }
        #endregion
    }
}
