using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.FileSystem.Disk
{
    public class DiskPath : IPath
    {
        #region Implementation of IPath
        /// <summary>
        /// Combines the specified paths.
        /// </summary>
        /// <param name="a">The first path.</param>
        /// <param name="b">The second path.</param>
        public string Combine(string a, string b)
        {
            return Path.Combine(a, b);
        }
        /// <summary>
        /// Combines the specified parts.
        /// </summary>
        /// <param name="parts">The parts.</param>
        public string Combine(params string[] parts)
        {
            return Path.Combine(parts);
        }
        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="path">The path.</param>
        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
        /// <summary>
        /// Gets the name of the file without the file extension.
        /// </summary>
        /// <param name="path">The path.</param>
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        /// <summary>
        /// Determines whether the specified path has extension.
        /// </summary>
        /// <param name="path">The path.</param>
        public bool HasExtension(string path)
        {
            return Path.HasExtension(path);
        }
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="path">The path.</param>
        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }
        #endregion
    }
}
