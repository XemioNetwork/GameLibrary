using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpConnection : IConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpConnection"/> class.
        /// </summary>
        /// <param name="tcpClient">The TCP client.</param>
        /// <param name="delay">The delay.</param>
        public TcpConnection(TcpClient tcpClient, TcpDelay delay)
        {
            this._tcpClient = tcpClient;
            this._tcpClient.NoDelay = (delay == TcpDelay.None);
            
            this._serializer = new PackageSerializer();
            this.Stream = tcpClient.GetStream();
        }
        #endregion

        #region Fields
        private readonly TcpClient _tcpClient;
        private readonly PackageSerializer _serializer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        #endregion Properties

        #region IConnection Member
        /// <summary>
        /// Gets the IP.
        /// </summary>
        public IPAddress IP
        {
            get { return ((IPEndPoint)this._tcpClient.Client.LocalEndPoint).Address; }
        }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        public bool Connected
        {
            get { return this._tcpClient.Connected; }
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
    }
}
