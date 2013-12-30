using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Formats.Binary;
using Xemio.GameLibrary.Content.Formats.Fallback;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Content.Serialization.Automatic;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content
{
    [Require(typeof(ImplementationManager))]

    public class SerializationManager : IComponent
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Dictionary<Type, List<Type>> _typeCache;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializationManager"/> class.
        /// </summary>
        public SerializationManager()
        {
            this._typeCache = new Dictionary<Type, List<Type>>();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the base types and interfaces.
        /// </summary>
        /// <param name="type">The type.</param>
        private IEnumerable<Type> GetBaseTypesAndInterfaces(Type type)
        {
            if (this._typeCache.ContainsKey(type))
                return this._typeCache[type];

            var types = new List<Type> {type};

            if (!type.IsInterface && !type.IsValueType && !type.IsArray)
            {
                Type currentType = type;
                while (currentType != typeof(object))
                {
                    types.Add(currentType);
                    currentType = currentType.BaseType;
                }
            }

            //Add interfaces
            types.AddRange(type.GetInterfaces());

            //If the specified type isn't an interface or value type,
            //add object as the base type for all reference types.
            if (!type.IsInterface && !type.IsValueType)
            {
                types.Add(typeof(object));
            }

            this._typeCache.Add(type, types);

            return types;
        } 
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IReader GetReader(Type type)
        {
            foreach (Type baseType in this.GetBaseTypesAndInterfaces(type))
            {
                IReader reader = XGL.Components
                    .Get<ImplementationManager>()
                    .Get<Type, IReader>(baseType);

                if (reader != null)
                    return reader;
            }

            return new AutomaticSerializer(type);
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private IWriter GetWriter(Type type)
        {
            foreach (Type baseType in this.GetBaseTypesAndInterfaces(type))
            {
                IWriter writer = XGL.Components
                    .Get<ImplementationManager>()
                    .Get<Type, IWriter>(baseType);

                if (writer != null)
                    return writer;
            }

            return new AutomaticSerializer(type);
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
            return (T)this.Load(typeof (T),reader);
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
                logger.Info("Initializing the format reader for {0} failed. Fallback will take place.", format.GetType().Name);
                return this.Load(type, new FallbackReader(stream, ex));
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
                logger.InfoException("Initializing the format writer for " + format.GetType().Name + " failed. Fallback will take place.", ex);
                this.Save(value, new FallbackWriter(stream, ex));
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
