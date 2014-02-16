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

    public class MessageServerLogic : ServerLogic<MessageRequest>
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a console message.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(IServer server, MessageRequest package, IConnection sender)
        {
            var sync = XGL.Components.Get<Syncput>();
            var syncConnection = (SyncputConnection)sender;

            var prefix = string.Format("<{0}> ", syncConnection.Player.Name);
            sync.Console.WriteLine(prefix + package.Message);

            server.Send(new MessagePackage(syncConnection.Player.Name, package.Message));
        }
        #endregion
    }
}
