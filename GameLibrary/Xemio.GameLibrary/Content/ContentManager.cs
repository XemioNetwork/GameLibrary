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
            this._contentMappings = new Dictionary<string, object>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, object> _contentMappings;
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
        public IContentReader GetReader(Type type)
        {
            return XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IContentReader>(type);
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
        public IContentWriter GetWriter(Type type)
        {
            return XGL.Components
                .Get<ImplementationManager>()
                .Get<Type, IContentWriter>(type);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentReader">The content reader.</param>
        private object Load(string fileName, IContentReader contentReader)
        {
            IFileSystem fileSystem = XGL.Components.Get<IFileSystem>();
            if (!this._contentMappings.ContainsKey(fileName))
            {
                using (Stream stream = fileSystem.Open(fileName))
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
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <typeparam name="TReader">The type of the reader.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        public virtual T Load<T, TReader>(string fileName) where TReader : IContentReader, new()
        {
            return (T)this.Load(fileName, new TReader());
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
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
            IFileSystem fileSystem = XGL.Components.Get<IFileSystem>();
            using (Stream stream = fileSystem.Create(fileName))
            {
                IContentWriter contentWriter = this.GetWriter(value.GetType());
                BinaryWriter writer = new BinaryWriter(stream);

                contentWriter.Write(writer, value);
            }
        }
        #endregion
    }
}
