using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Testing
{
    public class TestServer : Server
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestServer"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public TestServer(IServerProtocol protocol) : base(protocol)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the package assembly.
        /// </summary>
        public override Assembly PackageAssembly
        {
            get { return typeof(TestServer).Assembly; }
        }
        #endregion
    }
}
