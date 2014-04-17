using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Events.Servers;

namespace Xemio.GameLibrary.Network.Intercetors
{
    public interface IServerInterceptor
    {
        /// <summary>
        /// Intercepts the client joined process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptChannelOpened(ChannelOpenedEvent evt);
        /// <summary>
        /// Intercepts the client left process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptChannelClosed(ChannelClosedEvent evt);
        /// <summary>
        /// Intercepts the received process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptReceived(ServerReceivedPackageEvent evt);
        /// <summary>
        /// Intercepts the sending process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptSending(ServerSendingPackageEvent evt);
        /// <summary>
        /// Intercepts the sent process.
        /// </summary>
        /// <param name="evt">The event.</param>
        void InterceptSent(ServerSentPackageEvent evt);
    }
}
