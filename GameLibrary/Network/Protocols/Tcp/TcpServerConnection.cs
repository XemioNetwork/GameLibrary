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
using Xemio.GameLibrary.Network.Protocols.Streamed;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpServerConnection : StreamedServerChannelProtocol
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServerConnection"/> class.
        /// </summary>
        /// <param name="tcpClient">The TCP client.</param>
        /// <param name="delay">The delay.</param>
        public TcpServerConnection(TcpClient tcpClient, TcpDelay delay)
        {
            this._tcpClient = tcpClient;
            this._tcpClient.NoDelay = (delay == TcpDelay.None);
            this._address = ((IPEndPoint)this._tcpClient.Client.LocalEndPoint).Address;

            this._stream = tcpClient.GetStream();
        }
        #endregion

        #region Fields
        private readonly TcpClient _tcpClient;
        private readonly IPAddress _address;
        private readonly Stream _stream;
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

        #region IServerConnection Member
        /// <summary>
        /// Gets the ip address.
        /// </summary>
        public override IPAddress Address
        {
            get { return this._address; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IServerChannel"/> is connected.
        /// </summary>
        public override bool Connected
        {
            get { return this._tcpClient.Connected; }
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public override void Close()
        {
            this._tcpClient.Close();
        }
        #endregion
    }
}
