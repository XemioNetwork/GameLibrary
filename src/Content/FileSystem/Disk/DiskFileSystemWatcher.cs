using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.FileSystem.Disk
{
    internal class DiskFileSystemWatcher
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskFileSystemWatcher" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="path">The path.</param>
        /// <param name="listener">The listener.</param>
        public DiskFileSystemWatcher(DiskFileSystem fileSystem, string path, IFileSystemListener listener)
        {
            this.Listener = listener;

            string directoryName = fileSystem.Path.GetDirectoryName(path);

            this._watcher = new FileSystemWatcher(path) {IncludeSubdirectories = true};
            this._watcher.Changed += (sender, args) => this.Listener.OnChanged(directoryName + "/" + args.Name, args.Name);
            this._watcher.Created += (sender, args) => this.Listener.OnCreated(directoryName + "/" + args.Name, args.Name);
            this._watcher.Deleted += (sender, args) => this.Listener.OnDeleted(directoryName + "/" + args.Name, args.Name);
            this._watcher.Renamed += (sender, args) =>
            {
                this.Listener.OnDeleted(directoryName + "/" + args.OldName, args.OldName);
                this.Listener.OnCreated(directoryName + "/" + args.Name, args.Name);
            };

            this._watcher.EnableRaisingEvents = true;
        }
        #endregion

        #region Fields
        private readonly FileSystemWatcher _watcher;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the listener.
        /// </summary>
        public IFileSystemListener Listener { get; private set; }
        #endregion
    }
}
