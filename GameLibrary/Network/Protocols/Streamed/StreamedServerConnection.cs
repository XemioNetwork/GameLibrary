using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Streamed
{
    public abstract class StreamedServerConnection : StreamedSerializer, IServerConnection
    {
        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected abstract Stream Stream { get; }
        #endregion

        #region Implementation of IServerConnection
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets a value indicating whether the sender is connected.
        /// </summary>
        public abstract bool Connected { get; }
        /// <summary>
        /// Gets the internet address.
        /// </summary>
        public abstract IPAddress Address { get; }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public abstract void Disconnect();
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void Send(Package package)
        {
            try
            {
                this.Serialize(package, this.Stream);
            }
            catch (ObjectDisposedException)
            {
                throw new ConnectionClosedException(this);
            }
            catch (IOException)
            {
                throw new ConnectionClosedException(this);
            }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public virtual Package Receive()
        {
            try
            {
                return this.Deserialize(this.Stream);
            }
            catch (IOException)
            {
                throw new ConnectionClosedException(this);
            }
        }
        #endregion
    }
}
