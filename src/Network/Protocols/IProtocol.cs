using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Network.Protocols
{
    public interface IProtocol : ILinkable<string>
    {
        /// <summary>
        /// Starts the protocol and connects corresponding to the site it is being created.
        /// </summary>
        /// <param name="url">The url.</param>
        void Open(string url);
        /// <summary>
        /// Stops the protocol.
        /// </summary>
        void Close();
    }
}
