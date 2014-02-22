using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Network.Handlers.Attributes;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Handlers
{
    public class ReflectedServerHandler : ReflectedHandler<IServerHandlerAttribute>, IServerHandler
    {
        #region Implementation of IServerHandler
        /// <summary>
        /// Gets the type of the package.
        /// </summary>
        public Type PackageType
        {
            get { return typeof(Package); }
        }
        /// <summary>
        /// Called when a client joined the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public void OnClientJoined(IServer server, IServerConnection connection)
        {
            this.Invoke<OnClientJoinedAttribute>(server, connection);
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public void OnClientLeft(IServer server, IServerConnection connection)
        {
            this.Invoke<OnClientLeftAttribute>(server, connection);
        }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public void OnReceive(IServer server, Package package, IServerConnection sender)
        {
            this.Invoke<OnReceiveAttribute>(method => this.Matches(method, package), server, package, sender);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void OnSending(IServer server, Package package, IServerConnection receiver)
        {
            this.Invoke<OnSendingAttribute>(method => this.Matches(method, package), server, package, receiver);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void OnSent(IServer server, Package package, IServerConnection receiver)
        {
            this.Invoke<OnSentAttribute>(method => this.Matches(method, package), server, package, receiver);
        }
        #endregion
    }
}
