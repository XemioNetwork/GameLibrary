using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Formats.Xml;
using Xemio.GameLibrary.Content.Serialization.Automatic;
using Xemio.GameLibrary.Plugins.Implementations;
using BinaryReader = System.IO.BinaryReader;
using BinaryWriter = System.IO.BinaryWriter;

namespace Xemio.GameLibrary.Network.Packages
{
    public class PackageBuffer
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the specified package buffered into the network stream to prevent several packages from being sent.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="stream">The stream.</param>
        public void Serialize(Package package, Stream stream)
        {
            var buffer = new MemoryStream();
            var binaryWriter = new BinaryWriter(buffer);
            var serializer = XGL.Components.Require<SerializationManager>();

            binaryWriter.Write(package.Id);
            serializer.Save(package, buffer);

            byte[] data = buffer.ToArray();
            stream.Write(data, 0, data.Length);
            
            logger.Trace("Sending [{0}]", BitConverter.ToString(data));
        }
        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public Package Deserialize(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);
            var implementations = XGL.Components.Require<ImplementationManager>();
            var serializer = XGL.Components.Require<SerializationManager>();

            int packageId = binaryReader.ReadInt32();
            Type type = implementations.GetType<int, Package>(packageId);

            return (Package)serializer.Load(type, stream);
        }
        #endregion
    }
}
