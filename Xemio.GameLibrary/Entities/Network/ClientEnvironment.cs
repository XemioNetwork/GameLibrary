using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Entities.Network.Updates;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Entities.Network
{
    public class ClientEnvironment : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEnvironment"/> class.
        /// </summary>
        public ClientEnvironment()
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You can not create a client environment on the server side.");
            }

            PackageHandler handler = XGL.GetComponent<PackageHandler>();
            handler.Subscribe<Package>(this.OnReceivePackage);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the client.
        /// </summary>
        public Client Client
        {
            get { return XGL.GetComponent<Client>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the client receives a package.
        /// </summary>
        /// <param name="package">The package.</param>
        protected virtual void OnReceivePackage(Package package)
        {
            IWorldUpdate update = package as IWorldUpdate;
            if (update != null)
            {
                update.Apply(this);
            }
        }
        #endregion
    }
}
