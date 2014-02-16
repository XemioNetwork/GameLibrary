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

    public class MessageClientLogic : ClientLogic<MessagePackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a console message.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, MessagePackage package)
        {
            var sync = XGL.Components.Get<Syncput>();
            var prefix = string.Format("<{0}> ", package.PlayerName);

            sync.Console.WriteLine(prefix + package.Message);
        }
        #endregion
    }
}
