using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Streams;

namespace Xemio.GameLibrary.Content
{
    public static class Compressor
    {
        #region Methods
        /// <summary>
        /// Compresses the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static Stream Compress(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Compress);
        }
        /// <summary>
        /// Decompresses the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static Stream Decompress(Stream stream)
        {
            var memory = new MemoryStream();
            using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
            {
                gzip.CopyTo(memory);
            }

            memory.Position = 0;

            return memory;
        }
        #endregion

        #region Extension Methods
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        public static T LoadCompressed<T>(this SerializationManager serializer, string fileName, IFormat format)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();
            using (Stream stream = fileSystem.Open(fileName))
            {
                return serializer.LoadCompressed<T>(stream, format);
            }
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public static T LoadCompressed<T>(this SerializationManager serializer, Stream stream, IFormat format)
        {
            using (Stream decompressed = Compressor.Decompress(stream))
            {
                return serializer.Load<T>(decompressed, format);
            }
        }
        #endregion
    }
}
