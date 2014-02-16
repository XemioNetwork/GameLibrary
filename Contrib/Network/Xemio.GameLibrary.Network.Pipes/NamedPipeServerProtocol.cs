using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Network.Protocols;

namespace Xemio.GameLibrary.Network.Pipes
{
    public class NamedPipeServerProtocol : IServerProtocol
    {
        #region Fields
        private string _name;
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return "pipe"; }
        }
        #endregion

        #region Implementation of IProtocol
        /// <summary>
        /// Opens the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Open(string url)
        {
            this._name = url;
        }
        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
        }
        #endregion

        #region Implementation of IServerProtocol
        /// <summary>
        /// Accepts a connection.
        /// </summary>
        public IServerConnection AcceptConnection()
        {
            return new NamedPipeServerConnection(this._name);
        }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        public Server Server { get; set; }
        #endregion
    }
}
