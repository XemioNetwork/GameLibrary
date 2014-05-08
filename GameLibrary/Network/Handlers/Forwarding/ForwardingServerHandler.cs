using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Logging;
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
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public override void OnReceive(ServerChannel sender, Package package)
        {
            if (package is IForwarded == false)
                return;

            var forwardedPackage = (IForwarded)package;

            switch (forwardedPackage.Options)
            {
                case ForwardingOptions.All:
                    {
                        logger.Trace("Forwarding {0} from {1} to {2} clients.", package.GetType().Name, sender.Address, sender.Server.Connections.Count);
                        sender.Server.Send(package);
                    }
                    break;
                case ForwardingOptions.AllOther:
                    {
                        IList<ServerChannel> connections = sender.Server.Connections.Where(f => f != sender).ToList();
                        logger.Trace("Forwarding {0} from {1} to {2} clients.", package.GetType().Name, sender.Address, connections.Count);

                        foreach (ServerChannel channel in connections)
                        {
                            channel.Send(package);
                        }
                    }
                    break;
                case ForwardingOptions.Traceback:
                    {
                        logger.Trace("Forwarding {0} from {1} to sender.", package.GetType().Name, sender.Address);
                        sender.Send(package);
                    }
                    break;
            }

            base.OnReceive(sender, package);
        }
        #endregion
    }
}
