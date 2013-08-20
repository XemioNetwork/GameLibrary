using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content
{
    public class ContentManager : IComponent
    {
        #region Private Methods
        /// <summary>
        /// Gets the format for the specified file extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        private IFormat GetFormat(string extension)
        {
            return XGL.Components
                .Get<ImplementationManager>()
                .Get<string, IFormat>(extension);
        }
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IContentReader GetReader(Type type)
        {
            IContentReader reader = XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IContentReader>(type, false);

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
        private IContentWriter GetWriter(Type type)
        {
            IContentWriter writer = XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IContentWriter>(type, false);

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
        public T Load<T>(string fileName)
        {
            IFileSystem fileSystem = XGL.Components.Get<IFileSystem>();
            using (Stream stream = fileSystem.Open(fileName))
            {
                return this.Load<T>(stream, Path.GetExtension(fileName));
            }
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        public T Load<T>(Stream stream)
        {
            return this.Load<T, BinaryFormat>(stream);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <typeparam name="TFormat">The type of the format.</typeparam>
        /// <param name="stream">The stream.</param>
        public T Load<T, TFormat>(Stream stream) where TFormat : IFormat, new()
        {
            return this.Load<T>(stream, new TFormat().Id);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="formatName">Name of the format.</param>
        public T Load<T>(Stream stream, string formatName)
        {
            return (T)this.Load(typeof(T), stream, formatName);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        public object Load(Type type, Stream stream)
        {
            return this.Load(type, stream, new BinaryFormat().Id);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="formatName">Name of the format.</param>
        public object Load(Type type, Stream stream, string formatName)
        {
            IFormat format = this.GetFormat(formatName);
            IFormatReader formatReader = format.CreateReader(stream);

            return this.Load(type, formatReader);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="formatReader">The format reader.</param>
        public virtual object Load(Type type, IFormatReader formatReader)
        {
            IContentReader contentReader = this.GetReader(type);
            return contentReader.Read(formatReader);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        public void Save<TFormat>(object value, Stream stream) where TFormat : IFormat, new()
        {
            this.Save(value, stream, new TFormat().Id);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Save(object value, string fileName)
        {
            IFileSystem fileSystem = XGL.Components.Get<IFileSystem>();
            using (Stream stream = fileSystem.Create(fileName))
            {
                this.Save(value, stream, Path.GetExtension(fileName));
            }
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        public void Save(object value, Stream stream)
        {
            this.Save<BinaryFormat>(value, stream);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="formatName">Name of the format.</param>
        public void Save(object value, Stream stream, string formatName)
        {
            IFormat format = this.GetFormat(formatName);
            IFormatWriter formatWriter = format.CreateWriter(stream);

            this.Save(value, formatWriter);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formatWriter">The format writer.</param>
        public virtual void Save(object value, IFormatWriter formatWriter)
        {
            IContentWriter contentWriter = this.GetWriter(value.GetType());
            contentWriter.Write(formatWriter, value);
        }
        #endregion
    }
}
