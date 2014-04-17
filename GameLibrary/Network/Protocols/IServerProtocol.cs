using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IServerProtocol : IProtocol
    {
        /// <summary>
        /// Accepts a new channel.
        /// </summary>
        IServerChannelProtocol AcceptChannel();
        /// <summary>
        /// Sets the server.
        /// </summary>
        Server Server { set; }
    }
}
