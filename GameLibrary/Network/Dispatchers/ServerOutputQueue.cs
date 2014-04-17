using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events.Clients;
using Xemio.GameLibrary.Network.Events.Servers;

namespace Xemio.GameLibrary.Network.Dispatchers
{
    internal class ServerOutputQueue : OutputQueue
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerOutputQueue" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public ServerOutputQueue(ServerChannel channel) : base(channel.Protocol)
        {
            this.Channel = channel;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the channel.
        /// </summary>
        public ServerChannel Channel { get; private set; }
        #endregion

        #region Overrides of OutputQueue
        /// <summary>
        /// Called when the connection was lost.
        /// </summary>
        protected override void OnLostConnection()
        {
            var eventManager = XGL.Components.Require<IEventManager>();
            eventManager.Publish(new ChannelClosedEvent(this.Channel));
        }
        #endregion
    }
}
