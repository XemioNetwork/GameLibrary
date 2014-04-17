using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Config.Validation;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem.Disk;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Config.Installation
{
    public class ContentInstaller : AbstractInstaller
    {
        #region Properties
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public IFormat Format { get; set; }
        /// <summary>
        /// Gets or sets the tracking.
        /// </summary>
        public bool IsTrackingEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the content cache is enabled.
        /// </summary>
        public bool IsCachingEnabled { get; set; }
        #endregion

        #region Implementation of IInstaller
        /// <summary>
        /// Sets up the dependencies to other installers.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public override void SetupDependencies(DependencyManager manager)
        {
            manager.Dependency(() => new EventInstaller());
            manager.Dependency(() => new SerializationInstaller());
            manager.Dependency(() => new FileSystemInstaller(new DiskFileSystem()));
        }
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            var content = new ContentManager();
            content.Format = this.Format;
            content.IsCachingEnabled = this.IsCachingEnabled;

            if (this.IsTrackingEnabled)
            {
                content.EnableTracking();
            }

            catalog.Install(content);
        }
        #endregion
    }
}
