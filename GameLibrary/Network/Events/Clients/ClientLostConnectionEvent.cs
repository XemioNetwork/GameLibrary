using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events.Clients
{
    public class ClientLostConnectionEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLostConnectionEvent"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientLostConnectionEvent(Client client)
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
