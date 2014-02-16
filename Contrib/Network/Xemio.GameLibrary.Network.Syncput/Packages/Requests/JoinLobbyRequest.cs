using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Syncput.Core;

namespace Xemio.GameLibrary.Network.Syncput.Packages.Requests
{
    public class JoinLobbyRequest : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinLobbyRequest"/> class.
        /// </summary>
        public JoinLobbyRequest()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinLobbyRequest"/> class.
        /// </summary>
        /// <param name="playerName">The name.</param>
        /// <param name="frameIndex">Index of the frame.</param>
        public JoinLobbyRequest(string playerName, long frameIndex)
        {
            this.PlayerName = playerName;
            this.FrameIndex = frameIndex;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string PlayerName { get; private set; }
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        public long FrameIndex { get; private set; }
        #endregion
    }
}
