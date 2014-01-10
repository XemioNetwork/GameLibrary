using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.FileSystem
{
    public interface IFileSystemListener
    {
        /// <summary>
        /// Called when a file system entry changed.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        void OnChanged(string fullPath, string name);
        /// <summary>
        /// Called when a file system entry was created.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        void OnCreated(string fullPath, string name);
        /// <summary>
        /// Called when a file system entry was deleted.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        void OnDeleted(string fullPath, string name);
    }
}
