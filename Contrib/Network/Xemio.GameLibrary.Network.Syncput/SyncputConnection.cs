using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Protocols.Tcp;
using Xemio.GameLibrary.Network.Syncput.Core;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class SyncputConnection : NestedConnection<IConnection>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncputConnection"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public SyncputConnection(IConnection connection) : base(connection)
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player { get; internal set; }
        #endregion
    }
}
