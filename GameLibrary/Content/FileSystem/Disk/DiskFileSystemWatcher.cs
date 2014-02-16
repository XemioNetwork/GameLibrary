using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;

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

            const int retryLimit = 5;
            var timeout = TimeSpan.FromMilliseconds(500);

            this._watcher = new FileSystemWatcher(path) { IncludeSubdirectories = true };

            this._watcher.Changed += (sender, args) => Retry.Limited(retryLimit, timeout, () =>
                this.Listener.OnChanged(Path.GetFullPath(args.Name), args.Name));

            this._watcher.Created += (sender, args) => Retry.Limited(retryLimit, timeout, () => 
                this.Listener.OnCreated(Path.GetFullPath(args.Name), args.Name));

            this._watcher.Deleted += (sender, args) => Retry.Limited(retryLimit, timeout, () => 
                this.Listener.OnDeleted(Path.GetFullPath(args.Name), args.Name));

            this._watcher.Renamed += (sender, args) => Retry.Limited(retryLimit, timeout, () =>
            {
                this.Listener.OnDeleted(Path.GetFullPath(args.OldName), args.OldName);
                this.Listener.OnCreated(Path.GetFullPath(args.Name), args.Name);
            });

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
