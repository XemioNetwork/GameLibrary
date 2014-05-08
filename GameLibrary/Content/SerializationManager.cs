using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Formats.None;
using Xemio.GameLibrary.Content.Layouts;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content
{
    [Require(typeof(IImplementationManager))]

    public class SerializationManager : IComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets a reader or writer for the specified type.
        /// </summary>
        /// <typeparam name="T">The reader or writer type.</typeparam>
        /// <param name="type">The type.</param>
        private T Get<T>(Type type) where T : class, ILinkable<Type>
        {
            foreach (Type baseType in Reflection.GetInheritedTypes(type))
            {
                T instance = XGL.Components
                    .Get<IImplementationManager>()
                    .Get<Type, T>(baseType);

                if (instance != null)
                    return instance;
            }

            return new AutomaticLayoutSerializer(type) as T;
        }
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IContentReader GetContentReader(Type type)
        {
            return this.Get<IContentReader>(type);
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IContentWriter GetContentWriter(Type type)
        {
            return this.Get<IContentWriter>(type);
        }
        #endregion
        
        #region Methods
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
        /// Loads the specified reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        public T Load<T>(IFormatReader reader)
        {
            return (T)this.Load(typeof(T),reader);
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
            IContentReader contentReader = this.GetContentReader(type);
            if (contentReader.BypassFormat)
            {
                format = Format.None;
            }

            logger.Trace("Loading [{0}] from stream.", type.Name);
            using (IFormatReader formatReader = format.CreateReader(stream))
            {
                return contentReader.Read(formatReader);
            }
        }
        /// <summary>
        /// Loads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        public object Load(Type type, IFormatReader reader)
        {
            return this.GetContentReader(type).Read(reader);
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
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            IContentWriter contentWriter = this.GetContentWriter(value.GetType());
            if (contentWriter.BypassFormat)
            {
                format = Format.None;
            }

            logger.Trace("Writing [{0}] to stream.", value);
            using (IFormatWriter formatWriter = format.CreateWriter(stream))
            {
                contentWriter.Write(formatWriter, value);
            }
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        public void Save(object value, IFormatWriter writer)
        {
            this.GetContentWriter(value.GetType()).Write(writer, value);
        }
        #endregion
    }
}
