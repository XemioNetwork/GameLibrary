using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Network.Protocols;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Network.Events;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Network
{
    public abstract class Client : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IClientProtocol protocol)
        {
            this.Protocol = protocol;
            this.Protocol.Client = this;

            this.PackageManager = new PackageManager(this.PackageAssembly);
            this.StartLoop();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        public IClientProtocol Protocol { get; private set; }
        /// <summary>
        /// Gets the package manager.
        /// </summary>
        public PackageManager PackageManager { get; private set; }
        /// <summary>
        /// Gets the package assembly.
        /// </summary>
        public abstract Assembly PackageAssembly { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the client loop.
        /// </summary>
        private void StartLoop()
        {
            Task loopTask = new Task(this.ClientLoop);
            loopTask.Start();
        }
        /// <summary>
        /// Listens to the specified protocol and receives packages.
        /// </summary>
        private void ClientLoop()
        {
            while (this.Active)
            {
                Package package = this.Protocol.Receive();
                if (package != null)
                {
                    EventManager eventManager = ComponentManager.Get<EventManager>();
                    eventManager.Send(new ReceivedPackageEvent(package));
                }
            }
        }
        /// <summary>
        /// Sends the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void Send(Package package)
        {
            this.Protocol.Send(package);

            EventManager eventManager = ComponentManager.Get<EventManager>();
            eventManager.Send(new SentPackageEvent(package));
        }
        #endregion
    }
}
