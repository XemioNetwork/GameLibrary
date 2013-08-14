using System;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary
{
    internal class FluentConfiguration : Configuration
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfiguration"/> class.
        /// </summary>
        public FluentConfiguration()
        {
            this._fileSystem = new DiskFileSystem();
            this.SplashScreen = true;
        }
        #endregion

        #region Fields
        private IGraphicsInitializer _graphicsInitializer;
        private ISoundInitializer _soundInitializer;
        private IFileSystem _fileSystem;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to register the default components.
        /// </summary>
        public bool RegisterDefaultComponents { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show the splashscreen.
        /// </summary>
        public bool SplashScreen { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the configuration finished.
        /// </summary>
        public void ConfigurationFinished()
        {
            if (this.SplashScreen)
            {
                var splashScreen = new SplashScreen(this.Scenes);
                this.Scenes.Clear();
                this.Scenes.Add(splashScreen);
            }
        }
        /// <summary>
        /// Sets the specified file system.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public void Set(IFileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }
        /// <summary>
        /// Sets the graphics initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void Set(IGraphicsInitializer initializer)
        {
            this._graphicsInitializer = initializer;
        }
        /// <summary>
        /// Sets the sound initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void Set(ISoundInitializer initializer)
        {
            this._soundInitializer = initializer;
        }
        /// <summary>
        /// Registers the default components.
        /// </summary>
        public override void RegisterComponents()
        {
            if (this.RegisterDefaultComponents)
            {
                base.RegisterComponents();
            }
        }
        #endregion

        #region Overrides of Configuration
        /// <summary>
        /// Gets or sets the graphics initializer.
        /// </summary>
        public override IGraphicsInitializer GraphicsInitializer
        {
            get { return this._graphicsInitializer; }
        }
        /// <summary>
        /// Gets or sets the sound initializer.
        /// </summary>
        public override ISoundInitializer SoundInitializer
        {
            get { return this._soundInitializer; }
        }
        /// <summary>
        /// Gets the file system.
        /// </summary>
        public override IFileSystem FileSystem
        {
            get { return this._fileSystem; }
        }
        #endregion
    }
}