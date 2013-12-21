using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Handlers.Forwarding
{
    public interface IForwarded
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        ForwardingOptions Options { get; }
    }
}
