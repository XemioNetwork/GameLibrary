using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Events.Server
{
    public abstract class ServerPackageEvent : IEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerReceivedPackageEvent" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="connection">The connection.</param>
        protected ServerPackageEvent(IServer server, Package package, IServerConnection connection)
        {
            this.Server = server;
            this.Package = package;
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the server.
        /// </summary>
        public IServer Server { get; private set; }
        /// <summary>
        /// Gets the package.
        /// </summary>
        public Package Package { get; private set; }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IServerConnection Connection { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Converts the package to the specified type.
        /// </summary>
        public T As<T>() where T : Package
        {
            return this.Package as T;
        }
        #endregion
    }
}
