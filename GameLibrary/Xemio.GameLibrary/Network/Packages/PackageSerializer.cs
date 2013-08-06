using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Common.Extensions;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Network.Packages
{
    public class PackageSerializer
    {
        #region Methods
        /// <summary>
        /// Serializes the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize(Package package, BinaryWriter writer)
        {
            writer.Write(package.Id);
            writer.WriteInstance(package);
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public Package Deserialize(BinaryReader reader)
        {
            var implementations = XGL.Components.Get<ImplementationManager>();

            int packageId = reader.ReadInt32();
            Type packageType = implementations.GetType<int, Package>(packageId);

            return (Package)reader.ReadInstance(packageType);
        }
        #endregion
    }
}
