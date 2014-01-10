using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;

namespace Xemio.GameLibrary.Content.FileSystem
{
    [AbstractComponent]
    public interface IFileSystem : IComponent
    {
        /// <summary>
        /// Opens the specified file. Throws an exception if the file or the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        Stream Open(string fileName);
        /// <summary>
        /// Creates the specified file. Throws an exception if the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        Stream Create(string fileName);
        /// <summary>
        /// Deletes the specified file. Throws an exception if the file
        /// doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Delete(string fileName);
        /// <summary>
        /// Creates the specified directory and all parenting directories.
        /// </summary>
        /// <param name="path">The path.</param>
        void CreateDirectory(string path);
        /// <summary>
        /// Deletes the specified directory. Throws an exception if the
        /// directory doesn't exist.
        /// </summary>
        /// <param name="path">The path.</param>
        void DeleteDirectory(string path);
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        bool FileExists(string fileName);
        /// <summary>
        /// Determines wether the specified directory exists.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        bool DirectoryExists(string directoryName);
        /// <summary>
        /// Gets all files inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        string[] GetFiles(string directory);
        /// <summary>
        /// Gets all directories inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        string[] GetDirectories(string directory);
        /// <summary>
        /// Subscribes the specified file system watcher.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="listener">The listener.</param>
        void Subscribe(string path, IFileSystemListener listener);
        /// <summary>
        /// Unsubscribes the specified watcher.
        /// </summary>
        /// <param name="listener">The listener.</param>
        void Unsubscribe(IFileSystemListener listener);
        /// <summary>
        /// Gets the pathing tool for the file system implementation.
        /// </summary>
        IPath Path { get; }
    }
}
