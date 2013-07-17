using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Network.Synchronization;

namespace Xemio.GameLibrary.Entities.Network.Packages
{
    public class WorldExchangePackage : BinaryPackage, IWorldUpdate
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldExchangePackage"/> class.
        /// </summary>
        public WorldExchangePackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldExchangePackage"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public WorldExchangePackage(EntityEnvironment environment)
        {
            this.Create(environment);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the packages.
        /// </summary>
        public EntityCreationPackage[] Packages { get; set; }
        #endregion

        #region IWorldUpdate Member
        /// <summary>
        /// Creates a new world update for the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public void Create(EntityEnvironment environment)
        {
            List<Entity> createdEntities = environment
                .Where<Entity>(e => e.IsCreationSynced)
                .ToList();

            List<Entity> syncedEntities = environment
                .Where<Entity>(e => e.IsSynced)
                .ToList();

            this.Packages = new EntityCreationPackage[createdEntities.Count];
            for (int i = 0; i < createdEntities.Count; i++)
            {
                this.Packages[i] = new EntityCreationPackage(createdEntities[i]);
            }

            SyncHelper.Write(this.Stream, syncedEntities, Properties.All);
        }
        #endregion
    }
}
