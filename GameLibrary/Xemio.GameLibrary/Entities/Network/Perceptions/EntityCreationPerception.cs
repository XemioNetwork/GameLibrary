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
    public class EntityCreationPerception : ClientLogic<EntityCreationPackage>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreationPerception"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public EntityCreationPerception(EntityEnvironment environment)
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
        /// Called when the client receives an entity package.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="package">The package.</param>
        public override void OnReceive(Client client, EntityCreationPackage package)
        {
            Type type = Type.GetType(package.TypeName);

            EntityFactory factory = this.Environment.Factory;
            Entity entity = factory.CreateEntity(type, package.EntityId);

            this.Environment.Add(entity);
        }
        #endregion
    }
}
