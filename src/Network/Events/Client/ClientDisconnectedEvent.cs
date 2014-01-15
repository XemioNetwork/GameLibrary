using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events.Client
{
    public class ClientDisconnectedEvent : IInterceptableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDisconnectedEvent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientDisconnectedEvent(IClient client)
        {
            this.Client = client;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the client.
        /// </summary>
        public IClient Client { get; private set; }
        #endregion

        #region Implementation of IInterceptableEvent
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }
        #endregion
    }
}
