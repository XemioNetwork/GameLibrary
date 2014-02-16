using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Syncput.Packages
{
    public class MessagePackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePackage"/> class.
        /// </summary>
        public MessagePackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePackage"/> class.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="message">The message.</param>
        public MessagePackage(string playerName, string message)
        {
            this.PlayerName = playerName;
            this.Message = message;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePackage"/> class.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="format">The format.</param>
        /// <param name="parameters">The parameters.</param>
        public MessagePackage(string playerName, string format, params object[] parameters)
            : this(playerName, string.Format(format, parameters))
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string PlayerName { get; private set; }
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }
        #endregion
    }
}
