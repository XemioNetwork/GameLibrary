using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpClientProtocol : IClientProtocol
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClientProtocol"/> class.
        /// </summary>
        public TcpClientProtocol() 
            : this(TcpDelay.None)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClientProtocol"/> class.
        /// </summary>
        /// <param name="delay">The delay.</param>
        public TcpClientProtocol(TcpDelay delay)
        {
            this._serializer = new PackageSerializer();
            this._delay = delay;
        }
        #endregion

        #region Fields
        private readonly PackageSerializer _serializer;
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
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public void Connect(string ip, int port)
        {
            this._tcpClient = new TcpClient
                                  {
                                      NoDelay = (this._delay == TcpDelay.None)
                                  };

            this._tcpClient.Connect(IPAddress.Parse(ip), port);
            this.Stream = this._tcpClient.GetStream();

            while (!this._tcpClient.Connected)
            {
            }
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public void Disconnect()
        {
            this._tcpClient.Close();
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            this._serializer.Serialize(package, this.Stream);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return this._serializer.Deserialize(this.Stream);
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
    }
}
