using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events.Server
{
    public class ClientJoinedEvent : IInterceptableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientJoinedEvent" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public ClientJoinedEvent(IServer server, IServerConnection connection)
        {
            this.Server = server;
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the server.
        /// </summary>
        public IServer Server { get; private set; }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IServerConnection Connection { get; private set; }
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
            this.Connection.Disconnect();
        }
        #endregion
    }
}
