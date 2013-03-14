using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Entities.Network.Updates
{
    public class EntityPackage : Package, IWorldUpdate
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPackage"/> class.
        /// </summary>
        public EntityPackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPackage"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityPackage(Entity entity)
        {
            Type type = entity.GetType();

            this.ID = entity.ID;
            this.TypeName = type.AssemblyQualifiedName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        public string TypeName { get; set; }
        #endregion

        #region IWorldUpdate Member
        /// <summary>
        /// Applies the specified snapshot.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public void Apply(EntityEnvironment environment)
        {
            Type type = Type.GetType(this.TypeName);

            EntityFactory factory = environment.Factory;
            Entity entity = factory.CreateLocalEntity(type, this.ID);

            environment.Add(entity);
        }
        #endregion
    }
}
