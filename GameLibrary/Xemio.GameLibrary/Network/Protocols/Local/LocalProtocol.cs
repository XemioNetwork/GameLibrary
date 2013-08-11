using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    public class LocalProtocol : IServerProtocol, IConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalProtocol"/> class.
        /// </summary>
        private LocalProtocol()
        {
            this._localSleepTime = 1000;
            this._packageQueue = new CachedList<QueuePackage>();
        }
        #endregion

        #region Fields
        private readonly int _localSleepTime;
        private readonly CachedList<QueuePackage> _packageQueue;
        #endregion

        #region Static Fields
        public static readonly LocalProtocol ClientProtocol = new LocalProtocol();
        public static readonly LocalProtocol ServerProtocol = new LocalProtocol();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the simulated latency in milliseconds.
        /// </summary>
        public float SimulatedLatency { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        protected void Add(Package package)
        {
            var invoker = XGL.Components.Get<ThreadInvoker>();
            var queuePackage = new QueuePackage(package, this.SimulatedLatency);

            invoker.Invoke(() => this._packageQueue.Add(queuePackage));
        }
        #endregion

        #region IClientProtocol Member
        /// <summary>
        /// Sets the client.
        /// </summary>
        public Client Client { get; set; }
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
            if (this == LocalProtocol.ServerProtocol)
            {
                LocalProtocol.ClientProtocol.Add(package);
            }
            else
            {
                LocalProtocol.ServerProtocol.Add(package);
            }
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public Package Receive()
        {
            while (this._packageQueue.Count == 0)
            {
                //Just needed to stress the CPU a little bit less.
                Thread.Sleep(1);
            }

            ThreadInvoker invoker = XGL.Components.Get<ThreadInvoker>();
            QueuePackage queuePackage = null;

            invoker.Invoke(() =>
            {
                queuePackage = this._packageQueue.FirstOrDefault();

                foreach (QueuePackage package in this._packageQueue)
                {
                    package.Time -= queuePackage.Time;
                }

                this._packageQueue.Remove(queuePackage);
            });

            Thread.Sleep((int)queuePackage.Time);

            return queuePackage.Package; 
        }
        #endregion

        #region IServerProtocol Member
        /// <summary>
        /// Sets the server.
        /// </summary>
        public Server Server { get; set; }
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
            while (LocalProtocol.ServerProtocol.Server.Connections.Count > 0)
            {
                Thread.Sleep(this._localSleepTime);
            }

            return LocalProtocol.ServerProtocol;
        }
        #endregion

        #region IConnection Member
        /// <summary>
        /// Gets the IP.
        /// </summary>
        public IPAddress IP
        {
            get { return IPAddress.Parse("127.0.0.1"); }
        }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        public bool Connected
        {
            get { return true; }
        }
        #endregion
    }
}
