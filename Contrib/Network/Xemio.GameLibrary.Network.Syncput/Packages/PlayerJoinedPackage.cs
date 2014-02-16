using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Syncput.Core;

namespace Xemio.GameLibrary.Network.Syncput.Packages
{
    public class PlayerJoinedPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerJoinedPackage"/> class.
        /// </summary>
        public PlayerJoinedPackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerJoinedPackage"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public PlayerJoinedPackage(Player player)
        {
            this.Player = player;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player { get; private set; }
        #endregion
    }
}
