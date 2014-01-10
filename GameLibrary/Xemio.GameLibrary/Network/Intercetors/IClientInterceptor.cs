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
        void InterceptReceived(ClientReceivedPackageEvent evt);
        void InterceptBeginSend(ClientSendingPackageEvent evt);
        void InterceptSent(ClientSentPackageEvent evt);
    }
}
