using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Network.Protocols.Streamed;

namespace Xemio.GameLibrary.Network.Pipes
{
    public class NamedPipeClientProtocol : StreamedClientProtocol
    {
        #region Fields
        private NamedPipeClientStream _stream;
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public override string Id
        {
            get { return "pipe"; }
        }
        #endregion

        #region Implementation of IProtocol
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected override Stream Stream
        {
            get { return this._stream; }
        }
        /// <summary>
        /// Opens the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public override void Open(string url)
        {
            this._stream = new NamedPipeClientStream(url);
            this._stream.Connect();
        }
        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (this._stream == null)
            {
                throw new InvalidOperationException("The pipe client is not connected to a server. Make sure to call Open before closing the protocol.");
            }

            this._stream.Dispose();
            this._stream = null;
        }
        /// <summary>
        /// Gets a value indicating whether the pipe client is connected.
        /// </summary>
        public override bool Connected
        {
            get { return this._stream != null && this._stream.IsConnected; }
        }
        #endregion
    }
}
