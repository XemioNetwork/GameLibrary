using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Protocols.Local
{
    internal class LocalChannel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalChannel"/> class.
        /// </summary>
        public LocalChannel()
        {
            this.ClientQueue = new SignaledQueue<Package>();
            this.ServerQueue = new SignaledQueue<Package>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the client queue.
        /// </summary>
        public SignaledQueue<Package> ClientQueue { get; private set; } 
        /// <summary>
        /// Gets the server queue.
        /// </summary>
        public SignaledQueue<Package> ServerQueue { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="target">The target.</param>
        public void Send(Package package, LocalTarget target)
        {
            switch (target)
            {
                case LocalTarget.Client:
                    this.ClientQueue.Enqueue(package);
                    break;
                case LocalTarget.Server:
                    this.ServerQueue.Enqueue(package);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("target");
            }
        }
        #endregion
    }
}
