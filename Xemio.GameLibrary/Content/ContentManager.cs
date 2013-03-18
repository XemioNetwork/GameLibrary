using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.IO;

namespace Xemio.GameLibrary.Content
{
    public class ContentManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManager"/> class.
        /// </summary>
        public ContentManager()
        {
            this.FileSystem = new HDDFileSystem();

            this._contentMappings = new Dictionary<string, object>();
            this._readerMappings = new Dictionary<Type, IContentReader>();
            this._writerMappings = new Dictionary<Type, IContentWriter>();

            this.Register(new TextureReader());
            this.Register(new FontReader());
            this.Register(new SoundReader());
        }
        #endregion

        #region Fields
        private Dictionary<string, object> _contentMappings;

        private Dictionary<Type, IContentReader> _readerMappings;
        private Dictionary<Type, IContentWriter> _writerMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the file system.
        /// </summary>
        public IFileSystem FileSystem { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected IContentReader GetReader(Type type)
        {
            Type[] baseTypes = TypeHelper.GetBaseTypes(type);
            foreach (Type baseType in baseTypes)
            {
                if (this._readerMappings.ContainsKey(baseType))
                {
                    return this._readerMappings[baseType];
                }
            }

            string message = string.Format("No registered reader for '{0}'.", type);

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Send(new LoggingEvent(LoggingLevel.Error, message));

            throw new InvalidOperationException(message);
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected IContentWriter GetWriter(Type type)
        {
            Type[] baseTypes = TypeHelper.GetBaseTypes(type);
            foreach (Type baseType in baseTypes)
            {
                if (this._writerMappings.ContainsKey(baseType))
                {
                    return this._writerMappings[baseType];
                }
            }

            string message = string.Format("No registered writer for '{0}'.", type);

            EventManager eventManager = XGL.GetComponent<EventManager>();
            eventManager.Send(new LoggingEvent(LoggingLevel.Error, message));

            throw new InvalidOperationException(message);
        }
        /// <summary>
        /// Registers the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public virtual void Register(IContentReader reader)
        {
            this._readerMappings.Add(reader.Type, reader);
        }
        /// <summary>
        /// Registers the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public virtual void Register(IContentWriter writer)
        {
            this._writerMappings.Add(writer.Type, writer);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        public virtual T Load<T>(string fileName)
        {
            if (!this._contentMappings.ContainsKey(fileName))
            {
                IContentReader reader = this.GetReader(typeof(T));
                this._contentMappings[fileName] = reader.Read(fileName);
            }

            return (T)this._contentMappings[fileName];
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fileName">Name of the file.</param>
        public virtual void Save(object value, string fileName)
        {
            IContentWriter writer = this.GetWriter(value.GetType());
            writer.Write(fileName, value);
        }
        #endregion
    }
}
