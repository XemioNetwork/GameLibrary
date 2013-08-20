using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
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
        /// <param name="stream">The stream.</param>
        public void Serialize(Package package, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            ContentManager content = XGL.Components.Require<ContentManager>();

            writer.Write(package.Id);
            content.Save(package, stream);
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public Package Deserialize(Stream stream)
        {
            var reader = new BinaryReader(stream);

            var content = XGL.Components.Require<ContentManager>();
            var implementations = XGL.Components.Require<ImplementationManager>();

            int packageId = reader.ReadInt32();
            Type packageType = implementations.GetType<int, Package>(packageId);

            return (Package)content.Load(packageType, stream);
        }
        #endregion
    }
}
