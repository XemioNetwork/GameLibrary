using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Syncput.Core;

namespace Xemio.GameLibrary.Network.Syncput.Packages
{
    public class InitializationPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationPackage"/> class.
        /// </summary>
        public InitializationPackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializationPackage"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="maxPlayers">The max players.</param>
        /// <param name="players">The players.</param>
        public InitializationPackage(int seed, int maxPlayers, IList<Player> players)
        {
            this.Seed = seed;
            this.MaxPlayers = maxPlayers;
            this.Players = players;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the seed.
        /// </summary>
        public int Seed { get; private set; }
        /// <summary>
        /// Gets the max players.
        /// </summary>
        public int MaxPlayers { get; private set; }
        /// <summary>
        /// Gets the players.
        /// </summary>
        public IList<Player> Players { get; private set; } 
        #endregion
    }
}
