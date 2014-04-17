using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IServerChannelProtocol : ISender
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        IPAddress Address { get; }
        /// <summary>
        /// Receives a package.
        /// </summary>
        Package Receive();
        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();
    }
}
