using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network
{
    public interface INestedConnection : IConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        IConnection Connection { get; }
    }
}
