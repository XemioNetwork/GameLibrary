using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Tcp
{
    public class TcpServerProtocol : IServerProtocol
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServerProtocol"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public TcpServerProtocol(int port)
        {
            this._listener = new TcpListener(IPAddress.Any, port);
            this._listener.Start();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServerProtocol"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="delay">The delay.</param>
        public TcpServerProtocol(int port, TcpDelay delay) : this(port)
        {
            this._delay = delay;
        }
        #endregion

        #region Fields
        private readonly TcpDelay _delay;
        private readonly TcpListener _listener;
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Sends the specified package to the specified receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(Package package, IConnection receiver)
        {
            TcpConnection connection = receiver as TcpConnection;
            if (connection != null)
            {
                this.Server.Serializer.Serialize(package, connection.Writer);
            }
        }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        /// <returns></returns>
        public IConnection AcceptConnection()
        {
            return new TcpConnection(
                this.Server.Serializer,
                this._listener.AcceptTcpClient(),
                this._delay);
        }
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Gets ot sets the server.
        /// </summary>
        public Server Server { get; set; }
        #endregion
    }
}
