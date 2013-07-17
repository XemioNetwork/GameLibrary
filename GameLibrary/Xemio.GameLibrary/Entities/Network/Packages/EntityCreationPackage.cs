using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Entities.Network.Packages
{
    public class EntityCreationPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreationPackage"/> class.
        /// </summary>
        public EntityCreationPackage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreationPackage"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public EntityCreationPackage(Entity entity)
        {
            Type type = entity.GetType();

            this.EntityId = entity.Id;
            this.TypeName = type.AssemblyQualifiedName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        public string TypeName { get; set; }
        #endregion
    }
}
