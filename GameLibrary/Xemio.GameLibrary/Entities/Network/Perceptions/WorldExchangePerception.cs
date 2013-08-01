using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Entities.Network.Packages;
using Xemio.GameLibrary.Network.Logic;

namespace Xemio.GameLibrary.Entities.Network.Perceptions
{
    public class WorldExchangePerception : ClientLogic<WorldExchangePackage>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldExchangePerception"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public WorldExchangePerception(EntityEnvironment environment)
        {
            this.Environment = environment;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the client receives a world exchange package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, WorldExchangePackage package)
        {
            foreach (EntityCreationPackage creationPackage in package.Packages)
            {
                client.Receive(creationPackage);
            }

            SyncHelper.Read(package.Stream, this.Environment);
        }
        #endregion
    }
}
