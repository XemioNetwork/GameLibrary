using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xemio.GameLibrary.Content.FileSystem;

namespace Xemio.GameLibrary.Content.Caching
{
    internal class ContentCache : IContentCache
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentCache"/> class.
        /// </summary>
        public ContentCache()
        {
            this._fileNameObjectMappings = new Dictionary<string, object>();
            this._reverseMappings = new Dictionary<object, string>();

            this._uri = new Uri(Assembly.GetExecutingAssembly().Location);
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, object> _fileNameObjectMappings;
        private readonly Dictionary<object, string> _reverseMappings;

        private readonly Uri _uri;
        #endregion

        #region Implementation of IContentCache
        /// <summary>
        /// Queries the specified file name.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        public T Query<T>(string fileName)
        {
            if (!this._fileNameObjectMappings.ContainsKey(fileName))
            {
                return default(T);
            }

            return (T)this._fileNameObjectMappings[fileName];
        }
        /// <summary>
        /// Stores the specified instance.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="instance">The instance.</param>
        public void Store(string fileName, object instance)
        {
            this._fileNameObjectMappings[fileName] = instance;
            this._reverseMappings[instance] = fileName;
        }
        /// <summary>
        /// Determines whether the cache contains the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool Contains(string fileName)
        {
            return this._fileNameObjectMappings.ContainsKey(Path.GetFullPath(fileName));
        }
        /// <summary>
        /// Determines whether the specified instance contains the instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public bool ContainsInstance(object instance)
        {
            return this._reverseMappings.ContainsKey(instance);
        }
        /// <summary>
        /// Evicts the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Evict(object instance)
        {
            if (this._reverseMappings.ContainsKey(instance))
            {
                this.Evict(this._reverseMappings[instance]);
            }
        }
        /// <summary>
        /// Evicts the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Evict(string fileName)
        {
            var fileSystem = XGL.Components.Require<IFileSystem>();
            var fullPath = fileSystem.Path.GetFullPath(fileName);

            lock (this._fileNameObjectMappings)
            {
                if (this.Contains(fullPath))
                {
                    this._reverseMappings.Remove(this._fileNameObjectMappings[fullPath]);
                    this._fileNameObjectMappings.Remove(fullPath);
                }
            }
        }
        /// <summary>
        /// Evicts all cached references of the specified type.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        public void EvictAll<T>()
        {
            List<object> instances = this._fileNameObjectMappings.Values.Where(i => i is T).ToList(); 
            foreach (object instance in instances)
            {
                this.Evict(instance);
            }
        }
        /// <summary>
        /// Evicts all cached references.
        /// </summary>
        public void EvictAll()
        {
            this._reverseMappings.Clear();
            this._fileNameObjectMappings.Clear();
        }
        /// <summary>
        /// Gets the name of the file for the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public string GetFileName(object instance)
        {
            if (!this._reverseMappings.ContainsKey(instance))
            {
                throw new InstanceNotCachedException(instance);
            }

            return this._uri
                .MakeRelativeUri(new Uri(this._reverseMappings[instance]))
                .ToString();
        }
        #endregion
    }
}
