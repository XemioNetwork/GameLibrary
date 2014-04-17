using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Network.Dispatchers;
using Xemio.GameLibrary.Network.Events.Servers;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network
{
    public class ServerChannel : IStorage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerChannel" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="protocol">The protocol.</param>
        public ServerChannel(Server server, IServerChannelProtocol protocol)
        {
            this._storage = new ObjectStorage {AutoParseStrings = false};

            this.Server = server;
            this.Protocol = protocol;

            this.Queue = new ServerOutputQueue(this);
            this.Dispatcher = new ServerPackageDispatcher(this);
        }
        #endregion

        #region Fields
        private readonly ObjectStorage _storage;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the server.
        /// </summary>
        public Server Server { get; private set; }
        /// <summary>
        /// Gets the protocol.
        /// </summary>
        internal IServerChannelProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the queue.
        /// </summary>
        internal OutputQueue Queue { get; private set; }
        /// <summary>
        /// Gets the dispatcher.
        /// </summary>
        internal ServerPackageDispatcher Dispatcher { get; private set; }
        /// <summary>
        /// Gets the internet address.
        /// </summary>
        public IPAddress Address
        {
            get { return this.Protocol.Address; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Closes this channel and disconnects the corresponding client.
        /// </summary>
        public void Close()
        {
            this.Protocol.Close();
        }
        /// <summary>
        /// Receives the next incoming package.
        /// </summary>
        public Package Receive()
        {
            return this.Protocol.Receive();
        }
        #endregion

        #region Implementation of ISender, IStorage
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public bool Connected
        {
            get { return this.Protocol.Connected; }
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            var eventManager = XGL.Components.Require<IEventManager>();

            var sendingEvent = new ServerSendingPackageEvent(package, this);
            eventManager.Publish(sendingEvent);

            if (!sendingEvent.IsCanceled)
            {
                this.Queue.Enqueue(package);
            }

            var sentEvent = new ServerSentPackageEvent(package, this);
            eventManager.Publish(sentEvent);
        }
        /// <summary>
        /// Stores the specified value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        public void Store<T>(T value)
        {
            this._storage.Store(typeof(T).AssemblyQualifiedName, value);
        }
        /// <summary>
        /// Retrieves a value of the specified type.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        public T Retrieve<T>()
        {
            return this._storage.Retrieve<T>(typeof(T).AssemblyQualifiedName);
        }
        #endregion
    }
}
