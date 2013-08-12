using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Logic.Forwarding
{
    public class ForwardingServerLogic : ServerLogic<Package>
    {
        #region Methods
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(IServer server, Package package, IConnection sender)
        {
            if (package is IForwarded == false)
                return;

            IForwarded forwardedPackage = (IForwarded)package;
            
            switch (forwardedPackage.Options)
            {
                case ForwardingOptions.All:
                    server.Send(package);
                    break;
                case ForwardingOptions.AllOther:
                    foreach (IConnection connection in server.Connections.Where(f => f != sender))
                    {
                        server.Send(package, connection);
                    }
                    break;
                case ForwardingOptions.Sender:
                    server.Send(package, sender);
                    break;
            }

            base.OnReceive(server, package, sender);
        }
        #endregion
    }
}
