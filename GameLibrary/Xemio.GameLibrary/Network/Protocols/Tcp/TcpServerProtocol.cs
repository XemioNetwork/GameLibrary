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
        public TcpServerProtocol(int port) : this(port, TcpDelay.None)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServerProtocol"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="delay">The delay.</param>
        public TcpServerProtocol(int port, TcpDelay delay)
        {
            this._serializer = new PackageSerializer();
            this._delay = delay;

            this._listener = new TcpListener(IPAddress.Any, port);
            this._listener.Start();
        }
        #endregion

        #region Fields
        private readonly PackageSerializer _serializer;

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
            receiver.Send(package);
        }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        /// <returns></returns>
        public IConnection AcceptConnection()
        {
            return new TcpConnection(
                this._serializer,
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
