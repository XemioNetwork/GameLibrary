using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Config.Validation;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem.Disk;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Config.Installers
{
    public class ContentInstaller : AbstractInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInstaller"/> class.
        /// </summary>
        public ContentInstaller()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInstaller" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        public ContentInstaller(IFormat format) : this(format, ContentTracking.Disabled)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInstaller"/> class.
        /// </summary>
        /// <param name="tracking">The tracking.</param>
        public ContentInstaller(ContentTracking tracking) : this(Content.Formats.Format.Xml, tracking)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentInstaller"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="tracking">The tracking.</param>
        public ContentInstaller(IFormat format, ContentTracking tracking)
        {
            this.Format = format;
            this.Tracking = tracking;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public IFormat Format { get; set; }
        /// <summary>
        /// Gets or sets the tracking.
        /// </summary>
        public ContentTracking Tracking { get; set; }
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
        /// Sets up the conditions for your installation to complete.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public override void SetupConditions(ValidationManager manager)
        {
            manager.Not(new RequireComponent<ContentManager>());
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

            if (this.Tracking == ContentTracking.Enabled)
            {
                content.EnableTracking();
            }

            catalog.Install(content);
        }
        #endregion
    }
}
