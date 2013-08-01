using System;
using System.IO;
using System.Linq;

namespace Xemio.GameLibrary.Content.FileSystem.Virtualization
{
    public class VirtualFileSystem : IVirtualFileSystem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFileSystem"/> class.
        /// </summary>
        public VirtualFileSystem()
        {
            this._root = new VirtualDirectory(".");
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFileSystem"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public VirtualFileSystem(string fileName) : this()
        {
            this.Load(fileName);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualFileSystem"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public VirtualFileSystem(Stream stream)
        {
            this._root = VirtualDirectory.FromStream(stream);
        }
        #endregion

        #region Fields
        private VirtualDirectory _root;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the directory for the specified path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private VirtualDirectory GetDirectory(string fileName)
        {
            return this.GetDirectory(new VirtualPath(fileName));
        }
        /// <summary>
        /// Gets the directory for the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="returnParent">if set to <c>true</c> the method returns the parent directory.</param>
        private VirtualDirectory GetDirectory(VirtualPath path, bool returnParent = true)
        {
            VirtualDirectory directory = this._root;
            int count = returnParent ? path.Count - 1 : path.Count;

            var parts = path.Take(count);
            foreach (string part in parts)
            {
                if (directory == null)
                    return null;

                directory = directory.GetDirectory(part);
            }

            return directory;
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
            VirtualPath path = new VirtualPath(fileName);
            VirtualDirectory directory = this.GetDirectory(path);

            VirtualFile file = directory.GetFile(path.Name);
            Stream stream = file.CreateStream();

            return stream;
        }
        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Stream Create(string fileName)
        {
            if (this.GetDirectory(fileName) == null)
            {
                //TODO: Create directory for the specified file (NOT WORKING ATM)
                this.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            VirtualPath path = new VirtualPath(fileName);
            VirtualDirectory directory = this.GetDirectory(path);

            VirtualFile file = new VirtualFile(path.Name);
            VirtualFile existingFile = directory.GetFile(file.Name);

            if (existingFile != null)
            {
                directory.Files.Remove(existingFile);
            }

            directory.Add(file);

            return file.CreateStream();
        }
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {
            VirtualPath path = new VirtualPath(fileName);
            VirtualDirectory directory = this.GetDirectory(path);

            VirtualFile file = directory.GetFile(path.Name);
            if (file != null)
            {
                directory.Files.Remove(file);
            }
        }
        /// <summary>
        /// Creates the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateDirectory(string path)
        {
            VirtualPath virtualPath = new VirtualPath(path);
            VirtualDirectory directory = this.GetDirectory(virtualPath);

            VirtualDirectory newDirectory = new VirtualDirectory(virtualPath.Name);
            VirtualDirectory existingDirectory = directory.GetDirectory(newDirectory.Name);

            if (existingDirectory != null)
            {
                directory.Directories.Remove(existingDirectory);
            }

            directory.Add(newDirectory);
        }
        /// <summary>
        /// Deletes the specified directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void DeleteDirectory(string path)
        {
            VirtualPath virtualPath = new VirtualPath(path);
            VirtualDirectory directory = this.GetDirectory(virtualPath);

            VirtualDirectory removedDirectory = directory.GetDirectory(virtualPath.Name);
            directory.Directories.Remove(removedDirectory);
        }
        /// <summary>
        /// Determines wether the specified file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public bool FileExists(string fileName)
        {
            VirtualPath path = new VirtualPath(fileName);
            VirtualDirectory directory = this.GetDirectory(path);
            
            return directory != null && directory.Files.Any(
                file => file.Name == path.Name);
        }
        /// <summary>
        /// Determines wether the specified directory exists.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public bool DirectoryExists(string directoryName)
        {
            VirtualPath path = new VirtualPath(directoryName);
            VirtualDirectory directory = this.GetDirectory(path);

            return directory != null && directory.Directories.Any(
                dir => dir.Name == path.Name);
        }
        /// <summary>
        /// Gets all files inside the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetFiles(string directory)
        {
            VirtualPath path = new VirtualPath(directory);
            VirtualDirectory virtualDirectory = this.GetDirectory(path, false);

            if (virtualDirectory != null)
            {
                return virtualDirectory.Files.Select(f => f.FullPath).ToArray();
            }

            return null;
        }
        /// <summary>
        /// Gets all directories inside the specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public string[] GetDirectories(string directory)
        {
            VirtualPath path = new VirtualPath(directory);
            VirtualDirectory virtualDirectory = this.GetDirectory(path, false);

            if (virtualDirectory != null)
            {
                return virtualDirectory.Directories.Select(f => f.FullPath).ToArray();
            }

            return null;
        }
        #endregion

        #region Implementation of IVirtualFileSystem
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    this._root = VirtualDirectory.FromStream(stream);
                }
            }
        }
        /// <summary>
        /// Saves the changes made to the file system.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Persist(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                this._root.Save(stream);
            }
        }
        #endregion
    }
}
