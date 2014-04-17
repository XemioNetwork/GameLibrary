using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Events.Clients;

namespace Xemio.GameLibrary.Network.Dispatchers
{
    internal class ClientOutputQueue : OutputQueue
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputQueue" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientOutputQueue(Client client) : base(client.Protocol)
        {
            this.Client = client;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the client.
        /// </summary>
        public Client Client { get; private set; }
        #endregion

        #region Overrides of OutputQueue
        /// <summary>
        /// Called when the connection was lost.
        /// </summary>
        protected override void OnLostConnection()
        {
            var eventManager = XGL.Components.Require<IEventManager>();
            eventManager.Publish(new ClientLostConnectionEvent(this.Client));
        }
        #endregion
    }
}
