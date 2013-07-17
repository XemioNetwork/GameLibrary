using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
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
        private TcpDelay _delay;
        private TcpClient _tcpClient;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the writer.
        /// </summary>
        public BinaryWriter Writer { get; private set; }
        /// <summary>
        /// Gets the reader.
        /// </summary>
        public BinaryReader Reader { get; private set; }
        #endregion

        #region IProtocol Member
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public void Connect(string ip, int port)
        {
            this._tcpClient = new TcpClient();
            this._tcpClient.NoDelay = (this._delay == TcpDelay.None);
            this._tcpClient.Connect(IPAddress.Parse(ip), port);

            this.Writer = new BinaryWriter(this._tcpClient.GetStream());
            this.Reader = new BinaryReader(this._tcpClient.GetStream());
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
            PackageManager packageManager = this.Client.PackageManager;
            packageManager.Serialize(package, this.Writer);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            PackageManager packageManager = this.Client.PackageManager;
            return packageManager.Deserialize(this.Reader);
        }
        #endregion

        #region IClientProtocol Member
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public Client Client { get; set; }
        #endregion
    }
}
