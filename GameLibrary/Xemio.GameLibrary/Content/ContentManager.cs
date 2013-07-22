using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content.Serialization;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Plugins.Implementations;

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
            this.FileSystem = new DefaultFileSystem();
            this._contentMappings = new Dictionary<string, object>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, object> _contentMappings;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the file system.
        /// </summary>
        public IFileSystem FileSystem { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the reader for the specified type.
        /// </summary>
        public IContentReader GetReader<T>()
        {
            return this.GetReader(typeof(T));
        }
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IContentReader GetReader(Type type)
        {
            var implementations = XGL.Components.Get<ImplementationManager>();
            var reader = implementations.Get<Type, IContentReader>(type);

            if (reader == null)
            {
                const string format = "No reader found for '{0}'.";
                string message = string.Format(format, type.FullName);

                throw new InvalidOperationException(message);
            }

            return reader;
        }
        /// <summary>
        /// Gets the writer for the specified type.
        /// </summary>
        public IContentWriter GetWriter<T>()
        {
            return this.GetWriter(typeof (T));
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IContentWriter GetWriter(Type type)
        {
            var implementations = XGL.Components.Get<ImplementationManager>();
            return implementations.Get<Type, IContentWriter>(type);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentReader">The content reader.</param>
        private object Load(string fileName, IContentReader contentReader)
        {
            if (!this._contentMappings.ContainsKey(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    this._contentMappings[fileName] = contentReader.Read(reader);
                }
            }

            return this._contentMappings[fileName];
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReader">The type of the reader.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        public virtual T Load<T, TReader>(string fileName) where TReader : IContentReader, new()
        {
            return (T)this.Load(fileName, new TReader());
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        public virtual T Load<T>(string fileName)
        {
            return (T)this.Load(fileName, this.GetReader<T>());
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fileName">Name of the file.</param>
        public virtual void Save(object value, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                IContentWriter contentWriter = this.GetWriter(value.GetType());
                BinaryWriter writer = new BinaryWriter(stream);

                contentWriter.Write(writer, value);
            }
        }
        #endregion
    }
}
