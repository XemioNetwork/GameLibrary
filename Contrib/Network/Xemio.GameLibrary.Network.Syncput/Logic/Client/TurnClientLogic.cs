using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Network.Logic;
using Xemio.GameLibrary.Network.Syncput.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Logic.Client
{
    using Client = Xemio.GameLibrary.Network.Client;

    public class TurnClientLogic : ClientLogic<TurnPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a turn.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(IClient client, TurnPackage package)
        {
            InputManager inputManager = XGL.Components.Get<InputManager>();
            foreach (RemoteListener listener in inputManager
                .GetListeners(package.PlayerIndex)
                .OfType<RemoteListener>())
            {
                listener.Add(package);
            }
        }
        #endregion
    }
}
