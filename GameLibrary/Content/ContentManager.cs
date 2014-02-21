using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    [Require(typeof(SerializationManager))]

    public class ContentManager : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Fields
        private readonly Dictionary<string, object> _cache;
        private readonly Dictionary<object, string> _reverseMappings;

        private readonly Uri _applicationUri;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManager"/> class.
        /// </summary>
        public ContentManager()
        {
            this._cache = new Dictionary<string, object>();
            this._reverseMappings = new Dictionary<object, string>();

            this._applicationUri = new Uri(Assembly.GetExecutingAssembly().Location);

            this.Format = Formats.Format.Binary;
        }
        #endregion

        #region Fields
        private ContentTracker _tracker;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public IFormat Format { get; set; }
        /// <summary>
        /// Gets a value indicating whether the content manager is tracking changes to the file system.
        /// </summary>
        public bool IsTracking
        {
            get { return this._tracker != null; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Caches the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fullPath">The full path.</param>
        private void Cache(object value, string fullPath)
        {
            this._cache[fullPath] = value;
            this._reverseMappings[value] = fullPath;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the specified file is cached.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool IsCached(string fileName)
        {
            return this._cache.ContainsKey(Path.GetFullPath(fileName));
        }
        /// <summary>
        /// Gets the file for the specified object.
        /// </summary>
        /// <param name="value">The value.</param>
        public string GetFileName(object value)
        {
            if (!this._reverseMappings.ContainsKey(value))
            {
                throw new InvalidOperationException(
                    "The file name for the specified value does not exist inside the file cache. " +
                    "Please load or save it first using the content manager.");
            }

            return this._applicationUri
                .MakeRelativeUri(new Uri(this._reverseMappings[value]))
                .ToString();
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Save(object value, string fileName)
        {
            logger.Debug("Writing [{0}] to file {1}.", value, fileName);

            var serializer = XGL.Components.Require<SerializationManager>();
            var fileSystem = XGL.Components.Get<IFileSystem>();

            this.DisableTracking();
            using (Stream stream = fileSystem.Create(fileName))
            {
                serializer.Save(value, stream, this.Format);
            }
            this.EnableTracking();

            lock (this._cache)
            {
                this.Cache(value, fileSystem.Path.GetFullPath(fileName));
            }
        }
        /// <summary>
        /// Removes the specified file name from the internal content cache.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Unload(string fileName)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();
            var fullPath = fileSystem.Path.GetFullPath(fileName);

            lock (this._cache)
            {
                if (this.IsCached(fullPath))
                {
                    this._reverseMappings.Remove(this._cache[fullPath]);
                    this._cache.Remove(fullPath);
                }
            }
        }
        /// <summary>
        /// Gets the resource for the specified file name.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        public ContentReference<T> Get<T>(string fileName)
        {
            return new ContentReference<T>(this, fileName);
        }
        /// <summary>
        /// Queries the specified file.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        public T Query<T>(string fileName)
        {
            var serializer = XGL.Components.Require<SerializationManager>();
            var fileSystem = XGL.Components.Require<IFileSystem>();
            var fullPath = fileSystem.Path.GetFullPath(fileName);

            lock (this._cache)
            {
                if (!this.IsCached(fullPath))
                {
                    logger.Debug("Loading file {0} as [{1}].", fileName, typeof (T));

                    using (Stream stream = fileSystem.Open(fileName))
                    {
                        this.Cache(serializer.Load<T>(stream, this.Format), fullPath);
                    }
                }
            }

            return (T)this._cache[fullPath];
        }
        /// <summary>
        /// Enables the content tracking.
        /// </summary>
        public void EnableTracking()
        {
            if (this._tracker == null)
            {
                this._tracker = new ContentTracker(this);

                var fileSystem = XGL.Components.Get<IFileSystem>();
                fileSystem.Subscribe(".", this._tracker);

                logger.Info("Enabled content tracking.");
            }
        }
        /// <summary>
        /// Disables the content tracking.
        /// </summary>
        public void DisableTracking()
        {
            if (this._tracker != null)
            {
                var fileSystem = XGL.Components.Get<IFileSystem>();
                fileSystem.Unsubscribe(this._tracker);

                this._tracker = null;

                logger.Info("Disabled content tracking.");
            }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.EnableTracking();
        }
        #endregion
    }
}
