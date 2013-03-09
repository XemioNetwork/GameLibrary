﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Network.Events
{
    public class ClientLeftEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLeftEvent"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ClientLeftEvent(IConnection connection)
        {
            this.Connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the connection.
        /// </summary>
        public IConnection Connection { get; private set; }
        #endregion
    }
}