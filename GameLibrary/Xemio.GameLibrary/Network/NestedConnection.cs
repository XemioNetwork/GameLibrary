﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network
{
    public abstract class NestedConnection<T> : INestedConnection where T : IConnection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NestedConnection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected NestedConnection(T connection)
        {
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public T Connection { get; private set; }
        #endregion
        
        #region Implementation of IClientProtocol
        /// <summary>
        /// Connects to the specified ip.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public virtual void Connect(string ip, int port)
        {
            this.Connection.Connect(ip, port);
        }
        /// <summary>
        /// Disconnects the client.
        /// </summary>
        public virtual void Disconnect()
        {
            this.Connection.Disconnect();
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public virtual void Send(Package package)
        {
            this.Connection.Send(package);
        }
        /// <summary>
        /// Receives a package.
        /// </summary>
        public virtual Package Receive()
        {
            return this.Connection.Receive();
        }
        /// <summary>
        /// Sets the client.
        /// </summary>
        public virtual Client Client
        {
            set { this.Connection.Client = value; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        public virtual bool Connected
        {
            get { return this.Connection.Connected; }
        }
        /// <summary>
        /// Gets the IP.
        /// </summary>
        public virtual IPAddress IP
        {
            get { return this.Connection.IP; }
        }
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public virtual float Latency
        {
            get { return this.Connection.Latency; }
            set { this.Connection.Latency = value; }
        }
        #endregion

        #region INestedConnection Member
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IConnection INestedConnection.Connection
        {
            get { return this.Connection; }
        }
        #endregion
    }
}