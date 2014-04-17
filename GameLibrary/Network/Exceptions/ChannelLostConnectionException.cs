using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network.Exceptions
{
    public class ChannelLostConnectionException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelLostConnectionException" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public ChannelLostConnectionException(IServerChannelProtocol channel) : base(string.Format("{0} disconnected.", channel.Address))
        {
            this.Channel = channel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the channel.
        /// </summary>
        public IServerChannelProtocol Channel { get; private set; }
        #endregion
    }
}
