using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpConnection : IConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpConnection"/> class.
        /// </summary>
        /// <param name="tcpClient">The TCP client.</param>
        public TcpConnection(PackageManager packageManager, TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
            this._packageManager = packageManager;

            this.Writer = new BinaryWriter(tcpClient.GetStream());
            this.Reader = new BinaryReader(tcpClient.GetStream());
        }
        #endregion

        #region Fields
        private TcpClient _tcpClient;
        private PackageManager _packageManager;
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
        /// Gets the writer.
        /// </summary>
        public BinaryWriter Writer { get; private set; }
        /// <summary>
        /// Gets the reader.
        /// </summary>
        public BinaryReader Reader { get; private set; }
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
            //Not needed, because the connection is only held server-sided.
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
            this._packageManager.Serialize(package, this.Writer);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            return this._packageManager.Deserialize(this.Reader);
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
