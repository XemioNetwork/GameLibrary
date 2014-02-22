using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Events.Client;
using Xemio.GameLibrary.Network.Events.Server;

namespace Xemio.GameLibrary.Network.Intercetors
{
    public interface IClientInterceptor
    {
        /// <summary>
        /// Intercepts the disconnecting process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptDisconnect(ClientDisconnectedEvent evt);
        /// <summary>
        /// Intercepts the receiving process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptReceived(ClientReceivedPackageEvent evt);
        /// <summary>
        /// Intercepts the sending process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptSending(ClientSendingPackageEvent evt);
        /// <summary>
        /// Intercepts the sent process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptSent(ClientSentPackageEvent evt);
    }
}
