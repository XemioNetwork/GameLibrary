using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Local;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Syncput.Logic.Client;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class SyncputClient : Client
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncputClient"/> class.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="protocol">The protocol.</param>
        public SyncputClient(string playerName, IClientProtocol protocol) : base(protocol)
        {
            this.PlayerName = playerName;

            this.Subscribe(new TurnClientLogic());
            this.Subscribe(new PlayerJoinedClientLogic());
            this.Subscribe(new PlayerLeftClientLogic());
            this.Subscribe(new StartGameClientLogic());
            this.Subscribe(new MessageClientLogic());
            this.Subscribe(new InitializationClientLogic());
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string PlayerName { get; private set; }
        #endregion
    }
}
