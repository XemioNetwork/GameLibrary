using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Protocols;
using System.Net;

namespace Xemio.GameLibrary.Network
{
    public interface IConnection : IClientProtocol
    {
        /// <summary>
        /// Gets the IP.
        /// </summary>
        IPAddress IP { get; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="IConnection"/> is connected.
        /// </summary>
        bool Connected { get; }
    }
}
