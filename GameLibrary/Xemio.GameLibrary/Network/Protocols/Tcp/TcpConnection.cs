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
        /// <param name="serializer">The serializer.</param>
        /// <param name="tcpClient">The TCP client.</param>
        /// <param name="delay">The delay.</param>
        public TcpConnection(PackageSerializer serializer, TcpClient tcpClient, TcpDelay delay)
        {
            this._tcpClient = tcpClient;
            this._tcpClient.NoDelay = (delay == TcpDelay.None);
            
            this._serializer = serializer;
            this.Stream = tcpClient.GetStream();
        }
        #endregion

        #region Fields
        private readonly TcpClient _tcpClient;
        private readonly PackageSerializer _serializer;
        #endregion

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
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        public bool Connected
        {
            get { return this._tcpClient.Connected; }
        }
        #endregion

        #region IClientProtocol Member
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public void Connect(string ip, int port)
        {
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
        /// Sets the client.
        /// </summary>
        public Client Client { get; set; }
        #endregion
    }
}
