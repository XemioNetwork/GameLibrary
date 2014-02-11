using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.FileSystem
{
    public interface IPath
    {
        /// <summary>
        /// Combines the specified paths.
        /// </summary>
        /// <param name="a">The first path.</param>
        /// <param name="b">The second path.</param>
        string Combine(string a, string b);
        /// <summary>
        /// Combines the specified parts.
        /// </summary>
        /// <param name="parts">The parts.</param>
        string Combine(params string[] parts);
        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="path">The path.</param>
        string GetFullPath(string path);
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        string GetFileName(string path);
        /// <summary>
        /// Gets the name of the file without the file extension.
        /// </summary>
        /// <param name="path">The path.</param>
        string GetFileNameWithoutExtension(string path);
        /// <summary>
        /// Determines whether the specified path has extension.
        /// </summary>
        /// <param name="path">The path.</param>
        bool HasExtension(string path);
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="path">The path.</param>
        string GetExtension(string path);
        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        string GetDirectoryName(string path);
    }
}
