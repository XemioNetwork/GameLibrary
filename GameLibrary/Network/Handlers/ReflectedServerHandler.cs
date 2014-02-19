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
    public class ReflectedServerHandler : IServerHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectedServerHandler"/> class.
        /// </summary>
        public ReflectedServerHandler()
        {
            Type type = this.GetType();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attributes = Reflection.GetCustomAttributes(method).OfType<IServerHandlerAttribute>().ToList();

                if (attributes.Count > 0)
                {
                    throw new InvalidOperationException("Handling multiple events inside one method is not supported.");
                }

                this.AddMethod(method, attributes.Single());
            }
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Adds the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="attribute">The attribute.</param>
        private void AddMethod(MethodInfo method, IServerHandlerAttribute attribute)
        {
            
        }
        #endregion

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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Called when a client left the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="connection">The connection.</param>
        public void OnClientLeft(IServer server, IServerConnection connection)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Called when the server receives a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="sender">The sender.</param>
        public void OnReceive(IServer server, Package package, IServerConnection sender)
        {
        }
        /// <summary>
        /// Called when the server is sending a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void OnBeginSend(IServer server, Package package, IServerConnection receiver)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Called when the server sent a package.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="package">The package.</param>
        /// <param name="receiver">The receiver.</param>
        public void OnSent(IServer server, Package package, IServerConnection receiver)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
