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

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpClientProtocol : IClientProtocol
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
            this._buffer = new PackageBuffer();
            this._delay = delay;
        }
        #endregion

        #region Fields
        private readonly PackageBuffer _buffer;
        private readonly TcpDelay _delay;

        private TcpClient _tcpClient;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        #endregion

        #region IProtocol Member
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Open(string url)
        {
            string[] segments = url.Split(":".ToCharArray());
            if (segments.Length != 2)
            {
                throw new ArgumentException("Invalid url [" + url + "]", "url");
            }

            string ip = segments[0];
            int port;

            if (!int.TryParse(segments[1], out port))
            {
                throw new ArgumentException("Invalid port for url [" + url + "]", "url");
            }

            this._tcpClient = new TcpClient
                                  {
                                      NoDelay = (this._delay == TcpDelay.None)
                                  };

            this._tcpClient.Connect(IPAddress.Parse(ip), port);
            this.Stream = this._tcpClient.GetStream();
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public void Close()
        {
            this._tcpClient.Close();
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            try
            {
                lock (this._buffer)
                {
                    this._buffer.Serialize(package, this.Stream);
                }
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
                return this._buffer.Deserialize(this.Stream);
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

        #region IClientProtocol Member
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IClientProtocol"/> is connected.
        /// </summary>
        public bool Connected
        {
            get { return this._tcpClient != null && this._tcpClient.Connected; }
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return "tcp"; }
        }
        #endregion
    }
}
