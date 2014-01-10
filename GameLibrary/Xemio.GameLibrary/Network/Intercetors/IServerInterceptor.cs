using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Events.Server;

namespace Xemio.GameLibrary.Network.Intercetors
{
    public interface IServerInterceptor
    {
        void InterceptClientJoined(ClientJoinedEvent evt);
        void InterceptClientLeft(ClientLeftEvent evt);

        void InterceptReceived(ServerReceivedPackageEvent evt);
        void InterceptBeginSend(ServerSendingPackageEvent evt);
        void InterceptSent(ServerSentPackageEvent evt);
    }
}
