using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Exceptions;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpConnection : IServerConnection
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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
            
            this._buffer = new PackageBuffer();

            this.Stream = tcpClient.GetStream();
            this.Address = ((IPEndPoint)this._tcpClient.Client.LocalEndPoint).Address;
        }
        #endregion

        #region Fields
        private readonly TcpClient _tcpClient;
        private readonly PackageBuffer _buffer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the stream.
        /// </summary>
        public Stream Stream { get; private set; }
        #endregion Properties

        #region IConnection Member
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets the ip address.
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IServerConnection"/> is connected.
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
            try
            {
                lock (this._buffer)
                {
                    this._buffer.Serialize(package, this.Stream);
                }
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
        public Package Receive()
        {
            Package package;
            try
            {
                package = this._buffer.Deserialize(this.Stream);
            }
            catch (IOException)
            {
                throw new ConnectionClosedException(this);
            }

            return package;
        }
        #endregion
    }
}
