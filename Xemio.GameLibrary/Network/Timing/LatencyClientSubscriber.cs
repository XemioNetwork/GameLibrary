﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Subscribers;

namespace Xemio.GameLibrary.Network.Timing
{
    public class LatencyClientSubscriber : ClientSubscriber<LatencyPackage>
    {
        #region Methods
        /// <summary>
        /// Called when the client receives a latency package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, LatencyPackage package)
        {
            client.Latency = package.Latency;
        }
        #endregion
    }
}