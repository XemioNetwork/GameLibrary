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
    }
}
