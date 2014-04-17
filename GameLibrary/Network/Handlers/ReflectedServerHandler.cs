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
        /// <param name="channel">The channel.</param>
        public void OnChannelOpened(ServerChannel channel)
        {
            this.Invoke<OnClientJoinedAttribute>(channel);
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void OnChannelClosed(ServerChannel channel)
        {
            this.Invoke<OnClientLeftAttribute>(channel);
        }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="package">The package.</param>
        public void OnReceive(ServerChannel sender, Package package)
        {
            this.Invoke<OnReceiveAttribute>(method => this.Matches(method, package), sender, package);
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public void OnSending(ServerChannel receiver, Package package)
        {
            this.Invoke<OnSendingAttribute>(method => this.Matches(method, package), receiver, package);
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="package">The package.</param>
        public void OnSent(ServerChannel receiver, Package package)
        {
            this.Invoke<OnSentAttribute>(method => this.Matches(method, package), receiver, package);
        }
        #endregion
    }
}
