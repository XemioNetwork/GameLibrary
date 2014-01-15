using System;
using NLog;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Network.Protocols
{
    public class ProtocolFactory
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Static Methods
        /// <summary>
        /// Generic creation method (internal).
        /// </summary>
        /// <param name="protocolUrl">The protocol URL.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid protocol URL. Protocol could not be created for [protocolUrl]
        /// or
        /// Invalid protocol URL. Protocol [protocolName] does not exist.
        /// </exception>
        private static T CreateProtocol<T>(string protocolUrl) where T : class, IProtocol
        {
            string[] segments = protocolUrl.Split(new[] {"://"}, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length != 2)
            {
                throw new InvalidOperationException("Invalid protocol URL. Protocol could not be created for [" + protocolUrl + "]");
            }

            string protocolName = segments[0];
            string url = segments[1];

            var implementations = XGL.Components.Get<IImplementationManager>();
            T protocol = implementations.GetNew<string, T>(protocolName);

            if (protocol == null)
            {
                throw new InvalidOperationException("Invalid protocol URL. Protocol [" + protocolName + "] does not exist.");
            }

            logger.Debug("Created {0} for {1}", protocol.GetType().Name, protocolUrl);
            protocol.Open(url);
            
            return protocol;
        }
        /// <summary>
        /// Creates a new server protocol.
        /// </summary>
        /// <param name="protocolUrl">The protocol URL.</param>
        public static IServerProtocol CreateServerProtocol(string protocolUrl)
        {
            return ProtocolFactory.CreateProtocol<IServerProtocol>(protocolUrl);
        }
        /// <summary>
        /// Creates a new client protocol.
        /// </summary>
        /// <param name="protocolUrl">The protocol URL.</param>
        public static IClientProtocol CreateClientProtocol(string protocolUrl)
        {
            return ProtocolFactory.CreateProtocol<IClientProtocol>(protocolUrl);
        }
        #endregion
    }
}
