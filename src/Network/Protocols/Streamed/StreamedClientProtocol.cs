using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Streamed
{
    public abstract class StreamedClientProtocol : StreamedSerializer, IClientProtocol
    {
        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected abstract Stream Stream { get; }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public abstract string Id { get; }
        #endregion

        #region Implementation of IProtocol
        /// <summary>
        /// Starts the protocol and connects corresponding to the site it is being created.
        /// </summary>
        /// <param name="url">The url.</param>
        public abstract void Open(string url);
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        public abstract void Close();
        #endregion

        #region Implementation of IClientProtocol
        /// <summary>
        /// Gets a value indicating whether the client is connected.
        /// </summary>
        public abstract bool Connected { get; }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            this.Serialize(package, this.Stream);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return base.Deserialize(this.Stream);
        }
        /// <summary>
        /// Sets the client.
        /// </summary>
        public Client Client { get; set; }
        #endregion
    }
}
