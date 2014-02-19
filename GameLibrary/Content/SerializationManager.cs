using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Formats.Corrupted;
using Xemio.GameLibrary.Content.Layouts;
using Xemio.GameLibrary.Content.Layouts.Generation;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
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
        private IReader GetReader(Type type)
        {
            return this.Get<IReader>(type);
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IWriter GetWriter(Type type)
        {
            return this.Get<IWriter>(type);
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
            try
            {
                logger.Trace("Loading [{0}] from stream.", type.Name);
                using (IFormatReader reader = format.CreateReader(stream))
                {
                    return this.Load(type, reader);
                }
            }
            catch (Exception ex)
            {
                logger.Info("Initializing the format reader for {0} failed: Format corrupted.", format.GetType().Name);
                return this.Load(type, new CorruptedReader(stream, ex));
            }
        }
        /// <summary>
        /// Loads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        public object Load(Type type, IFormatReader reader)
        {
            return this.GetReader(type).Read(reader);
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
            try
            {
                logger.Trace("Writing [{0}] to stream", value ?? "null");
                using (IFormatWriter writer = format.CreateWriter(stream))
                {
                    this.Save(value, writer);
                }
            }
            catch (Exception ex)
            {
                logger.InfoException("Initializing the format writer for " + format.GetType().Name + " failed: Format corrupted.", ex);
                this.Save(value, new CorruptedWriter(stream, ex));
            }
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        public void Save(object value, IFormatWriter writer)
        {
            this.GetWriter(value.GetType()).Write(writer, value);
        }
        #endregion
    }
}
