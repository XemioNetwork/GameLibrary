using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Handlers.Forwarding
{
    public enum ForwardingOptions
    {
        /// <summary>
        /// Sends the received package to all clients.
        /// </summary>
        All,
        /// <summary>
        /// Sends the received package to all clients except for the sender.
        /// </summary>
        AllOther,
        /// <summary>
        /// Sends the received package to its sender.
        /// </summary>
        Sender
    }
}
