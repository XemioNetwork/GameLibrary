using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
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
            var buffer = new MemoryStream();
            var bufferWriter = new BinaryWriter(buffer);

            var serializer = XGL.Components.Require<SerializationManager>();

            bufferWriter.Write(package.Id);
            serializer.Save(package, buffer);

            byte[] data = buffer.ToArray();
            stream.Write(data, 0, data.Length);
        }
        /// <summary>
        /// Deserializes the specified reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public Package Deserialize(Stream stream)
        {
            var reader = new BinaryReader(stream);

            var serializer = XGL.Components.Require<SerializationManager>();
            var implementations = XGL.Components.Require<ImplementationManager>();

            int packageId = reader.ReadInt32();
            Type packageType = implementations.GetType<int, Package>(packageId);

            return (Package)serializer.Load(packageType, stream);
        }
        #endregion
    }
}
