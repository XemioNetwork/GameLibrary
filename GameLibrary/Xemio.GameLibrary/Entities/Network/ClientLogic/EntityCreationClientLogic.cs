using System;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Entities.Network.Packages;
using Xemio.GameLibrary.Network.Logic;

namespace Xemio.GameLibrary.Entities.Network.ClientLogic
{
    public class EntityCreationClientLogic : ClientLogic<EntityCreationPackage>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreationClientLogic"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public EntityCreationClientLogic(EntityEnvironment environment)
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
            
            Entity entity = (Entity)Activator.CreateInstance(type);
            entity.EntityId = package.EntityId;

            this.Environment.Add(entity);
        }
        #endregion
    }
}
