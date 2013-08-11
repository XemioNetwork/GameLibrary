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
        public TcpServerProtocol() 
            : this(TcpDelay.None)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServerProtocol"/> class.
        /// </summary>
        /// <param name="delay">The delay.</param>
        public TcpServerProtocol(TcpDelay delay)
        {
            this._delay = delay;
        }
        #endregion

        #region Fields
        private readonly TcpDelay _delay;
        private TcpListener _listener;
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Hosts at the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        public void Host(int port)
        {
            if (this.Hosted)
                return;

            this._listener = new TcpListener(IPAddress.Any, port);
            this._listener.Start();

            this.Hosted = true;
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IServerProtocol"/> is hosted.
        /// </summary>
        public bool Hosted { get; private set; }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        /// <returns></returns>
        public IConnection AcceptConnection()
        {
            return new TcpConnection(
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
