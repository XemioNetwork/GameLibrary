using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.FileSystem.Disk;

namespace Xemio.GameLibrary.Config.Installation
{
    public class FileSystemInstaller : AbstractInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemInstaller"/> class.
        /// </summary>
        public FileSystemInstaller()
        {
            this.FileSystem = new DiskFileSystem();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemInstaller"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public FileSystemInstaller(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the file system.
        /// </summary>
        public IFileSystem FileSystem { get; set; }
        #endregion

        #region Implementation of IInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(this.FileSystem);
        }
        #endregion
    }
}
