using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalProtocol : IClientProtocol, IServerProtocol, IConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalProtocol"/> class.
        /// </summary>
        public LocalProtocol()
        {
            this._localSleepTime = 1000;
            this._packageQueue = new Queue<Package>();

            this.IP = IPAddress.Parse("127.0.0.1");
        }
        #endregion

        #region Fields
        private int _localSleepTime;
        private Queue<Package> _packageQueue;
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
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            this._packageQueue.Enqueue(package);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        /// <returns></returns>
        public Package Receive()
        {
            while (this._packageQueue.Count == 0)
            {
            }

            return this._packageQueue.Dequeue();
        }
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Sends the specified package to the specified receiver.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void Send(Package package, IConnection receiver)
        {
            this.Send(package);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public Package Receive(IConnection connection)
        {
            return this.Receive();
        }
        /// <summary>
        /// Accepts a new connection.
        /// </summary>
        public IConnection AcceptConnection()
        {
            while (this.Connected)
            {
                Thread.Sleep(this._localSleepTime);
            }

            this.Connected = true;
            return this;
        }
        #endregion

        #region IConnection Member
        /// <summary>
        /// Gets the IP.
        /// </summary>
        public IPAddress IP { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        public bool Connected { get; private set; }
        #endregion

        #region IClientProtocol Member
        /// <summary>
        /// Sets the client.
        /// </summary>
        public Client Client { get; set; }
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Sets the server.
        /// </summary>
        public Server Server { get; set; }
        #endregion
    }
}
