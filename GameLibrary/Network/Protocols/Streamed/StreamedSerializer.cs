using System;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Network.Protocols.Streamed
{
    public class StreamedSerializer
    {
        #region Fields
        private readonly object _writeSemaphore = new object();
        private readonly object _readSemaphore = new object();
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the specified package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="stream">The stream.</param>
        protected void Serialize(Package package, Stream stream)
        {
            var buffer = new MemoryStream();
            var binaryWriter = new BinaryWriter(buffer);
            var serializer = XGL.Components.Require<SerializationManager>();

            binaryWriter.Write(package.Id);
            serializer.Save(package, buffer);

            byte[] data = buffer.ToArray();
            lock (this._writeSemaphore)
            {
                stream.Write(data, 0, data.Length);
            }
        }
        /// <summary>
        /// Deserializes a package from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected Package Deserialize(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);
            var implementations = XGL.Components.Require<IImplementationManager>();
            var serializer = XGL.Components.Require<SerializationManager>();

            int packageId = binaryReader.ReadInt32();
            Type type = implementations.GetType<int, Package>(packageId);

            lock (this._readSemaphore)
            {
                return (Package) serializer.Load(type, stream);
            }
        }
        #endregion
    }
}
