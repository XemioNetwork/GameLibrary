using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.GameLibrary.Content.FileSystem.Extendable
{
    public class ExtendableFileSystem<T> : ExtendableFileSystem where T : IFileSystem, new()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendableFileSystem" /> class.
        /// </summary>
        public ExtendableFileSystem() : base(new T())
        {
        }
        #endregion
    }
    public class ExtendableFileSystem : IFileSystem, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendableFileSystem" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public ExtendableFileSystem(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
            this.Extensions = new List<IFileSystem>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the file system.
        /// </summary>
        public IFileSystem FileSystem { get; private set; }
        /// <summary>
        /// Gets the file systems.
        /// </summary>
        public IList<IFileSystem> Extensions { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Gets all file systems including the main file system.
        /// </summary>
        private IEnumerable<IFileSystem> FileSystems()
        {
            yield return this.FileSystem;
            foreach (IFileSystem extension in this.Extensions)
            {
                yield return extension;
            }
        } 
        #endregion

        #region Implementation of IFileSystem
        /// <summary>
        /// Opens the specified file. Throws an exception if the file or the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Stream Open(string fileName)
        {
            IFileSystem fileSystem = this.FileSystems().FirstOrDefault(fs => fs.FileExists(fileName));
            if (fileSystem == null)
            {
                throw new InvalidOperationException("File " + fileName + " not found.");
            }

            return fileSystem.Open(fileName);
        }
        /// <summary>
        /// Creates the specified file. Throws an exception if the
        /// containing directory doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Stream Create(string fileName)
        {
            return this.FileSystem.Create(fileName);
        }
        /// <summary>
        /// Deletes the specified file. Throws an exception if the file
        /// doesn't exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {
            foreach (IFileSystem fileSystem in this.FileSystems())
            {
                if (fileSystem.FileExists(fileName))
                {
                    fileSystem.Delete(fileName);
                }
            }
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
            foreach (IFileSystem fileSystem in this.FileSystems())
            {
                if (fileSystem.DirectoryExists(path))
                {
                    fileSystem.DeleteDirectory(path);
                }
            }
        }
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool FileExists(string fileName)
        {
            return this.FileSystems().Any(fileSystem => fileSystem.FileExists(fileName));
        }
        /// <summary>
        /// Determines wether the specified directory exists.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public bool DirectoryExists(string directoryName)
        {
            return this.FileSystems().Any(fileSystem => fileSystem.DirectoryExists(directoryName));
        }
        /// <summary>
        /// Gets all files inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetFiles(string directory)
        {
            return this.FileSystems()
                .Where(fileSystem => fileSystem.DirectoryExists(directory))
                .SelectMany(fileSystem => fileSystem.GetFiles(directory))
                .Distinct()
                .ToArray();
        }
        /// <summary>
        /// Gets all directories inside the specified directory. Returns an empty array
        /// if the directory doesn't exist.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetDirectories(string directory)
        {
            return this.FileSystems()
                .Where(fileSystem => fileSystem.DirectoryExists(directory))
                .SelectMany(fileSystem => fileSystem.GetDirectories(directory))
                .Distinct()
                .ToArray();
        }
        /// <summary>
        /// Subscribes the specified file system watcher.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="listener">The listener.</param>
        public void Subscribe(string path, IFileSystemListener listener)
        {
            foreach (IFileSystem fileSystem in this.FileSystems())
            {
                if (fileSystem.DirectoryExists(path))
                {
                    fileSystem.Subscribe(path, listener);
                }
            }
        }
        /// <summary>
        /// Unsubscribes the specified watcher.
        /// </summary>
        /// <param name="listener">The listener.</param>
        public void Unsubscribe(IFileSystemListener listener)
        {
            foreach (IFileSystem fileSystem in this.FileSystems())
            {
                fileSystem.Unsubscribe(listener);
            }
        }
        /// <summary>
        /// Gets the pathing tool for the file system implementation.
        /// </summary>
        public IPath Path
        {
            get { return this.FileSystem.Path; }
        }
        #endregion

        #region Implementation of IConstructable
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            var implementations = XGL.Components.Get<IImplementationManager>();
            if (implementations != null)
            {
                var extensions = implementations.All<string, IFileSystemExtension>();
                foreach (IFileSystemExtension extension in extensions)
                {
                    this.Extensions.Add(extension.FileSystem);
                }
            }
        }
        #endregion
    }
}
