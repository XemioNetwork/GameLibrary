using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols.Streamed;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpClientProtocol : StreamedClientProtocol
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClientProtocol"/> class.
        /// </summary>
        public TcpClientProtocol() : this(TcpDelay.None)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClientProtocol"/> class.
        /// </summary>
        /// <param name="delay">The delay.</param>
        public TcpClientProtocol(TcpDelay delay)
        {
            this._delay = delay;
        }
        #endregion

        #region Fields
        private readonly TcpDelay _delay;

        private TcpClient _tcpClient;
        private Stream _stream;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        protected override Stream Stream
        {
            get { return this._stream; }
        }
        #endregion

        #region IClientProtocol Member
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="url">The URL.</param>
        public override void Open(string url)
        {
            string[] segments = url.Split(":".ToCharArray());
            if (segments.Length != 2)
            {
                throw new ArgumentException(@"Invalid url [" + url + "]", "url");
            }

            string ip = segments[0];
            int port;

            if (!int.TryParse(segments[1], out port))
            {
                throw new ArgumentException(@"Invalid port for url [" + url + "]", "url");
            }

            this._tcpClient = new TcpClient
                                  {
                                      NoDelay = (this._delay == TcpDelay.None)
                                  };

            this._tcpClient.Connect(IPAddress.Parse(ip), port);
            this._stream = this._tcpClient.GetStream();
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public override void Close()
        {
            this._tcpClient.Close();
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IClientProtocol"/> is connected.
        /// </summary>
        public override bool Connected
        {
            get { return this._tcpClient != null && this._tcpClient.Connected; }
        }
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public override string Id
        {
            get { return "tcp"; }
        }
        #endregion
    }
}
