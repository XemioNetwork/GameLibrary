using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content.Caching;
using Xemio.GameLibrary.Content.Exceptions;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Content.Metadata;

namespace Xemio.GameLibrary.Content
{
    [Require(typeof(SerializationManager))]

    public class ContentManager : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion
        
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManager"/> class.
        /// </summary>
        public ContentManager()
        {
            this._cache = new ContentCache();
            this._metadata = new Dictionary<object, IMetadata>();
            this._includedDirectories = new List<string> {"."};

            this.Format = Formats.Format.Binary;
            this.IsCachingEnabled = true;
        }
        #endregion

        #region Fields
        private ContentTracker _tracker;

        private readonly ContentCache _cache;
        private readonly Dictionary<object, IMetadata> _metadata;

        private readonly List<string> _includedDirectories; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public IFormat Format { get; set; }
        /// <summary>
        /// Gets the cache.
        /// </summary>
        public IContentCache Cache
        {
            get { return this._cache; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether caching is enabled.
        /// </summary>
        public bool IsCachingEnabled { get; set; }
        /// <summary>
        /// Gets a value indicating whether the content manager is tracking changes to the file system.
        /// </summary>
        public bool IsTrackingEnabled
        {
            get { return this._tracker != null; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns a region without content tracking.
        /// </summary>
        private IDisposable Untracked()
        {
            return new ActionDisposable(this.DisableTracking, this.EnableTracking);
        }
        #endregion

        #region Methods
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
        /// Gets the resource for the specified file name.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="guid">The unique identifier.</param>
        public ContentReference<T> Get<T>(Guid guid)
        {
            return new ContentReference<T>(this, this.Find(guid));
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
            var metaPath = fileName + ".meta";

            lock (this._cache)
            {
                if (!this._cache.Contains(fullPath) || !this.IsCachingEnabled)
                {
                    logger.Debug("Loading file {0} as [{1}].", fileName, typeof(T));

                    using (Stream stream = fileSystem.Open(fileName))
                    {
                        this._cache.Store(fullPath, serializer.Load<T>(stream, this.Format));
                    }
                }
            }

            var result = this._cache.Query<T>(fullPath);

            //Cache the linked meta data, if an instance is loaded by the content manager.
            if (this.HasMetadata(result) == false &&
                fileSystem.FileExists(metaPath) &&
                typeof(IMetadata).IsAssignableFrom(typeof(T)) == false)
            {
                this.SetMetadata(result, this.Query<IMetadata>(metaPath));
            }

            return result;
        }
        /// <summary>
        /// Queries the specified file.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="guid">The unique identifier.</param>
        public T Query<T>(Guid guid)
        {
            string fileName = this.Find(guid);
            if (string.IsNullOrEmpty(fileName))
            {
                return default(T);
            }

            return this.Query<T>(fileName);
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
            string metaPath = fileName + ".meta";

            using (this.Untracked())
            {
                using (Stream stream = fileSystem.Create(fileName))
                {
                    serializer.Save(value, stream, this.Format);
                }
                
                lock (this._cache)
                {
                    this._cache.Store(fileSystem.Path.GetFullPath(fileName), value);
                }

                if (value is IMetadata)
                    return;

                //Initialize default meta data, if not meta data is specified for the
                //currently saved object.
                if (!this.HasMetadata(value))
                {
                    this.SetMetadata(value, new DefaultMetadata(value.GetType()));
                }

                this.Save(this.GetMetadata(value), metaPath);
            }
        }
        #endregion

        #region Private Metadata Methods
        /// <summary>
        /// Finds the file by the specified unique identifier.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="results">The results.</param>
        private void Find(Func<IMetadata, bool> predicate, string directory, List<string> results)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();

            results.AddRange(from fileName in fileSystem.GetFiles(directory)
                             where fileName.EndsWith(".meta")
                             let metadata = this.Query<IMetadata>(fileName)
                             where predicate(metadata)
                             select fileName.Substring(0, fileName.Length - 5));

            foreach (string subDirectory in fileSystem.GetDirectories(directory))
            {
                this.Find(predicate, subDirectory, results);
            }
        }
        #endregion

        #region Metadata Methods
        /// <summary>
        /// Includes the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void Include(string directory)
        {
            this._includedDirectories.Add(directory);
        }
        /// <summary>
        /// Excludes the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void Exclude(string directory)
        {
            this._includedDirectories.Remove(directory);
        }
        /// <summary>
        /// Finds the file by the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public string Find(Guid guid)
        {
            var results = this.Find(meta => meta.Guid == guid).ToList();
            if (results.Count > 1)
            {
                throw new DuplicateGuidException(guid);
            }
            if (results.Count == 0)
            {
                throw new GuidNotFoundException(guid);
            }

            return results.Single();
        }
        /// <summary>
        /// Finds all files containing the specified type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        public IEnumerable<string> Find<T>()
        {
            return this.Find(typeof(T));
        } 
        /// <summary>
        /// Finds all files containing the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public IEnumerable<string> Find(Type type)
        {
            return this.Find(meta => type.IsAssignableFrom(meta.Type));
        }
        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public IEnumerable<string> Find(Func<IMetadata, bool> predicate)
        {
            var results = new List<string>();
            foreach (string directory in this._includedDirectories)
            {
                this.Find(predicate, directory, results);
            }

            return results;
        }
        /// <summary>
        /// Stores the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>A value indicating, wether the instance already had a metadata instance.</returns>
        public void SetMetadata(object instance, IMetadata metadata)
        {
            this._metadata[instance] = metadata;
        }
        /// <summary>
        /// Gets the meta data for the specified file name.
        /// </summary>
        /// <typeparam name="T">The meta data type.</typeparam>
        /// <param name="instance">The instance.</param>
        public T GetMetadata<T>(object instance)
        {
            return (T)this.GetMetadata(instance);
        }
        /// <summary>
        /// Gets the meta data for the specified file name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public IMetadata GetMetadata(object instance)
        {
            if (this.HasMetadata(instance))
            {
                return this._metadata[instance];
            }

            return null;
        }
        /// <summary>
        /// Determines whether the specified instance has metadata.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public bool HasMetadata(object instance)
        {
            return this._metadata.ContainsKey(instance);
        }
        #endregion

        #region Tracking Methods
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
