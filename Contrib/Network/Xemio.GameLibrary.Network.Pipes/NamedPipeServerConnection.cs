using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols.Streamed;

namespace Xemio.GameLibrary.Network.Pipes
{
    public class NamedPipeServerConnection : StreamedServerConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedPipeServerConnection" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedPipeServerConnection(string name)
        {
            this._stream = new NamedPipeServerStream(name, PipeDirection.InOut, 32);
            this._stream.WaitForConnection();
        }
        #endregion

        #region Fields
        private readonly NamedPipeServerStream _stream;
        #endregion

        #region Overrides of StreamedServerConnection
        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public override void Disconnect()
        {
            this._stream.Disconnect();
            this._stream.Dispose();
        }
        /// <summary>
        /// Gets the address.
        /// </summary> <value>
        /// The address.
        /// </value>
        public override IPAddress Address
        {
            get { return IPAddress.Parse("127.0.0.1"); }
        }
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected override Stream Stream
        {
            get { return this._stream; }
        }
        /// <summary>
        /// Gets a value indicating whether the connection is established.
        /// </summary>
        public override bool Connected
        {
            get { return this._stream.IsConnected; }
        }
        #endregion
    }
}
