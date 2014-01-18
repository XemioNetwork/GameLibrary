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
        public TcpServerProtocol() : this(TcpDelay.None)
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
        /// Hosts at the specified url.
        /// </summary>
        /// <param name="url">The url.</param>
        public void Open(string url)
        {
            int port;
            if (!int.TryParse(url, out port))
            {
                throw new ArgumentException(@"Invalid port [" + url + "].", "url");
            }

            this._listener = new TcpListener(IPAddress.Any, port);
            this._listener.Start();
        }
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        public void Close()
        {
            this._listener.Stop();
        }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        /// <returns></returns>
        public IServerConnection AcceptConnection()
        {
            return new TcpServerConnection(this._listener.AcceptTcpClient(), this._delay);
        }
        /// <summary>
        /// Gets ot sets the server.
        /// </summary>
        public Server Server { get; set; }
        #endregion
        
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return "tcp"; }
        }
        #endregion
    }
}
