using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Logging;

namespace Xemio.GameLibrary.Content.FileSystem.Compression
{
    public class CompressedFileSystem<T> : CompressedFileSystem where T : IFileSystem, new()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedFileSystem{T}"/> class.
        /// </summary>
        public CompressedFileSystem() : base(new T())
        {
        }
        #endregion
    }

    public class CompressedFileSystem : IFileSystem
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedFileSystem"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public CompressedFileSystem(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the file system.
        /// </summary>
        public IFileSystem FileSystem { get; private set; }
        #endregion

        #region Implementation of IFileSystem
        /// <summary>
        /// Opens the specified file. Throws an exception if the file or the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Stream Open(string fileName)
        {
            try
            {
                using (Stream fileStream = this.FileSystem.Open(fileName))
                {
                    return Compressor.Decompress(fileStream);
                }
            }
            catch (InvalidDataException)
            {
                logger.Warn("Could not decompress {0}. Trying to read plain data from file.", fileName);
                return this.FileSystem.Open(fileName);
            }
        }
        /// <summary>
        /// Creates the specified file. Throws an exception if the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Stream Create(string fileName)
        {
            return Compressor.Compress(this.FileSystem.Create(fileName));
        }
        /// <summary>
        /// Deletes the specified file. Throws an exception if the file
        /// doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {
            this.FileSystem.Delete(fileName);
        }
        /// <summary>
        /// Creates the specified directory and all parenting directories.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateDirectory(string path)
        {
            this.FileSystem.CreateDirectory(path);
        }
        /// <summary>
        /// Deletes the specified directory. Throws an exception if the
        /// directory doesn't exist.
        /// </summary>
        /// <param name="path">The path.</param>
        public void DeleteDirectory(string path)
        {
            this.FileSystem.DeleteDirectory(path);
        }
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool FileExists(string fileName)
        {
            return this.FileSystem.FileExists(fileName);
        }
        /// <summary>
        /// Determines wether the specified directory exists.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public bool DirectoryExists(string directoryName)
        {
            return this.FileSystem.DirectoryExists(directoryName);
        }
        /// <summary>
        /// Gets all files inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetFiles(string directory)
        {
            return this.FileSystem.GetFiles(directory);
        }
        /// <summary>
        /// Gets all directories inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetDirectories(string directory)
        {
            return this.FileSystem.GetDirectories(directory);
        }

        /// <summary>
        /// Subscribes the specified file system watcher.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="watcher">The watcher.</param>
        public void Subscribe(string path, IFileSystemListener watcher)
        {
            this.FileSystem.Subscribe(path, watcher);
        }
        /// <summary>
        /// Unsubscribes the specified watcher.
        /// </summary>
        /// <param name="watcher">The watcher.</param>
        public void Unsubscribe(IFileSystemListener watcher)
        {
            this.FileSystem.Unsubscribe(watcher);
        }
        /// <summary>
        /// Gets the pathing tool for the file system implementation.
        /// </summary>
        public IPath Path
        {
            get { return this.FileSystem.Path; }
        }
        #endregion
    }
}
