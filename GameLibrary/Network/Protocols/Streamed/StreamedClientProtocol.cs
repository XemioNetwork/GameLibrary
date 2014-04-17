using System;
using System.IO;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Streamed
{
    public abstract class StreamedClientProtocol : BufferedSerializer, IClientProtocol
    {
        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected abstract Stream Stream { get; }
        #endregion

        #region Implementation of IClientProtocol
        /// <summary>
        /// Sets the client.
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public abstract string Id { get; }
        /// <summary>
        /// Gets a value indicating whether the client is connected.
        /// </summary>
        public abstract bool Connected { get; }
        /// <summary>
        /// Starts the protocol and connects corresponding to the site it is being created.
        /// </summary>
        /// <param name="url">The url.</param>
        public abstract void Open(string url);
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            try
            {
                this.Serialize(package, this.Stream);
            }
            catch (ObjectDisposedException)
            {
                throw new ClientLostConnectionException();
            }
            catch (IOException)
            {
                throw new ClientLostConnectionException();
            }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            try
            {
                return base.Deserialize(this.Stream);
            }
            catch (ObjectDisposedException)
            {
                throw new ClientLostConnectionException();
            }
            catch (IOException)
            {
                throw new ClientLostConnectionException();
            }
        }
        #endregion
    }
}
