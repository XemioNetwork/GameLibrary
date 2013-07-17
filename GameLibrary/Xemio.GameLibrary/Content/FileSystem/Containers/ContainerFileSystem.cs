using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xemio.GameLibrary.Content.FileSystem.Virtualization;

namespace Xemio.GameLibrary.Content.FileSystem.Containers
{
    public class ContainerFileSystem : IFileSystem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerFileSystem"/> class.
        /// </summary>
        public ContainerFileSystem()
        {
            this._fileCache = new Dictionary<string, VirtualFileSystem>();
            this.RootDirectory = ".";
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, VirtualFileSystem> _fileCache; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the root directory.
        /// </summary>
        public string RootDirectory { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Saves all changes.
        /// </summary>
        public void Save()
        {
            foreach (KeyValuePair<string, VirtualFileSystem> fsPair in this._fileCache)
            {
                fsPair.Value.Save(fsPair.Key);
            }
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        private string GetFileName(ContainerPath path)
        {
            return Path.Combine(this.RootDirectory, path.Container);
        }
        /// <summary>
        /// Gets the cached file system.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        private VirtualFileSystem GetCachedFileSystem(string container)
        {
            if (!this._fileCache.ContainsKey(container))
            {
                this._fileCache.Add(container, new VirtualFileSystem(container));
            }

            return this._fileCache[container];
        }
        #endregion

        #region Implementation of IFileSystem
        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Stream Open(string fileName)
        {
            ContainerPath path = new ContainerPath(fileName);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            return fileSystem.Open(path.FileName);
        }
        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Stream Create(string fileName)
        {
            ContainerPath path = new ContainerPath(fileName);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            return fileSystem.Create(path.FileName);
        }
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {
            ContainerPath path = new ContainerPath(fileName);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            fileSystem.Delete(path.FileName);
        }
        /// <summary>
        /// Creates the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateDirectory(string path)
        {
            ContainerPath containerPath = new ContainerPath(path);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(containerPath));

            fileSystem.CreateDirectory(containerPath.FileName);
        }
        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void DeleteDirectory(string path)
        {
            ContainerPath containerPath = new ContainerPath(path);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(containerPath));

            fileSystem.DeleteDirectory(containerPath.FileName);
        }
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool Exists(string fileName)
        {
            ContainerPath path = new ContainerPath(fileName);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            return fileSystem.Exists(path.FileName);
        }
        /// <summary>
        /// Gets all files inside the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetFiles(string directory)
        {
            ContainerPath path = new ContainerPath(directory);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            return fileSystem.GetFiles(path.FileName)
                .Select(f => path.Container + "://" + f)
                .ToArray();
        }
        /// <summary>
        /// Gets all directories inside the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetDirectories(string directory)
        {
            ContainerPath path = new ContainerPath(directory);
            VirtualFileSystem fileSystem = this.GetCachedFileSystem(this.GetFileName(path));

            return fileSystem.GetDirectories(path.FileName)
                .Select(f => path.Container + "://" + f)
                .ToArray();
        }
        #endregion
    }
}
