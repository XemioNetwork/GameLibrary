using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Network.Syncput.Packages;
using Xemio.GameLibrary.Network.Syncput.Packages.Requests;

namespace Xemio.GameLibrary.Network.Syncput.Turns
{
    public class TurnSender
    {
        #region Methods
        /// <summary>
        /// Requests a simulation.
        /// </summary>
        /// <param name="turnSequence">The turn sequence.</param>
        public void SendTurn(int turnSequence)
        {
            InputManager inputManager = XGL.Components.Get<InputManager>();

            IClient client = XGL.Components.Get<IClient>();
            PlayerInput localInput = inputManager.PlayerInputs.First();

            client.Send(new TurnRequest(localInput, turnSequence));
        }
        #endregion
    }
}
