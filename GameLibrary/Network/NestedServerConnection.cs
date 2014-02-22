using System.Net;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network
{
    public abstract class NestedServerConnection : INestedServerConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NestedServerConnection" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected NestedServerConnection(IServerConnection connection)
        {
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IServerConnection Connection { get; private set; }
        #endregion
        
        #region Implementation of IClientProtocl
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public virtual void Disconnect()
        {
            this.Connection.Disconnect();
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void Send(Package package)
        {
            this.Connection.Send(package);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public virtual Package Receive()
        {
            return this.Connection.Receive();
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IServerConnection"/> is connected.
        /// </summary>
        public virtual bool Connected
        {
            get { return this.Connection.Connected; }
        }
        /// <summary>
        /// Gets the Address.
        /// </summary>
        public virtual IPAddress Address
        {
            get { return this.Connection.Address; }
        }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public virtual float Latency
        {
            get { return this.Connection.Latency; }
            set { this.Connection.Latency = value; }
        }
        #endregion

        #region INestedConnection Member
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IServerConnection INestedServerConnection.Connection
        {
            get { return this.Connection; }
        }
        #endregion
    }
}
