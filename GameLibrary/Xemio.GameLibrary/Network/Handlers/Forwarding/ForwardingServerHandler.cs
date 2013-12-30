using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers.Forwarding
{
    public class ForwardingServerHandler : ServerHandler<Package>
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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

            var forwardedPackage = (IForwarded)package;
            
            switch (forwardedPackage.Options)
            {
                case ForwardingOptions.All:
                    logger.Trace("Forwarding {0} from {1} to {2} clients.", package.GetType().Name, sender.Address, server.Connections.Count);
                    server.Send(package);
                    break;
                case ForwardingOptions.AllOther:
                    IList<IConnection> connections = server.Connections.Where(f => f != sender).ToList();
                    logger.Trace("Forwarding {0} from {1} to {2} clients.", package.GetType().Name, sender.Address, connections.Count);

                    foreach (IConnection connection in connections)
                    {
                        server.Send(package, connection);
                    }
                    break;
                case ForwardingOptions.Sender:
                    logger.Trace("Forwarding {0} from {1} to sender.", package.GetType().Name, sender.Address);
                    server.Send(package, sender);
                    break;
            }

            base.OnReceive(server, package, sender);
        }
        #endregion
    }
}
