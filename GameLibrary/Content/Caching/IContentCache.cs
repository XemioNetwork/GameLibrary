using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Caching
{
    public interface IContentCache
    {
        /// <summary>
        /// Gets the name of the file for the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        string GetFileName(object instance);
        /// <summary>
        /// Queries the specified file name.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        T Query<T>(string fileName);
        /// <summary>
        /// Determines whether the cache contains the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        bool Contains(string fileName);
        /// <summary>
        /// Determines whether the specified instance contains the instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        bool ContainsInstance(object instance);
        /// <summary>
        /// Evicts the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Evict(object instance);
        /// <summary>
        /// Evicts the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Evict(string fileName);
        /// <summary>
        /// Evicts all cached references of the specified type.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        void EvictAll<T>();
        /// <summary>
        /// Evicts all cached references.
        /// </summary>
        void EvictAll();
        /// <summary>
        /// Stores the specified instance.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="instance">The instance.</param>
        void Store(string fileName, object instance);
    }
}
