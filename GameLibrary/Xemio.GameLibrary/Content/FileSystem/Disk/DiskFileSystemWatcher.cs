using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.FileSystem.Disk
{
    internal class DiskFileSystemWatcher
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskFileSystemWatcher" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="listener">The listener.</param>
        public DiskFileSystemWatcher(string path, IFileSystemListener listener)
        {
            this.Listener = listener;

            this._watcher = new FileSystemWatcher(path);
            this._watcher.Changed += (sender, args) => this.Listener.OnChanged(args.FullPath, args.Name);
            this._watcher.Created += (sender, args) => this.Listener.OnCreated(args.FullPath, args.Name);
            this._watcher.Deleted += (sender, args) => this.Listener.OnDeleted(args.FullPath, args.Name);
            this._watcher.Renamed += (sender, args) => this.Listener.OnRenamed(args.FullPath, args.OldFullPath, args.Name, args.OldName);
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
