using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content
{
    [Require(typeof(ImplementationManager))]

    public class SerializationManager : IComponent
    {
        #region Private Methods
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IReader GetReader(Type type)
        {
            IReader reader = XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IReader>(type);

            if (reader == null)
            {
                return new AutomaticSerializer(type);
            }

            return reader;
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IWriter GetWriter(Type type)
        {
            IWriter writer = XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IWriter>(type);

            if (writer == null)
            {
                return new AutomaticSerializer(type);
            }

            return writer;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        public T Load<T>(string fileName, IFormat format)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();
            using (Stream stream = fileSystem.Open(fileName))
            {
                return this.Load<T>(stream, format);
            }
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        public T Load<T>(Stream stream)
        {
            return this.Load<T>(stream, Format.Binary);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public T Load<T>(Stream stream, IFormat format)
        {
            return (T)this.Load(typeof(T), stream, format);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        public object Load(Type type, Stream stream)
        {
            return this.Load(type, stream, Format.Binary);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public object Load(Type type, Stream stream, IFormat format)
        {
            return this.Load(type, format.CreateReader(stream));
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="formatReader">The format reader.</param>
        public virtual object Load(Type type, IFormatReader formatReader)
        {
            return this.GetReader(type).Read(formatReader);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        public void Save(object value, string fileName, IFormat format)
        {
            var fileSystem = XGL.Components.Get<IFileSystem>();
            using (Stream stream = fileSystem.Create(fileName))
            {
                this.Save(value, stream, format);
            }
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        public void Save(object value, Stream stream)
        {
            this.Save(value, stream, Format.Binary);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public void Save(object value, Stream stream, IFormat format)
        {
            this.Save(value, format.CreateWriter(stream));
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatWriter">The format writer.</param>
        public virtual void Save(object value, IFormatWriter formatWriter)
        {
            this.GetWriter(value.GetType()).Write(formatWriter, value);
        }
        #endregion
    }
}
