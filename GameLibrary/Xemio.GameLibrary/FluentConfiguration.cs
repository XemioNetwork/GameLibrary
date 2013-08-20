using System;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Surfaces;
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
            this._gameLoop = new GameLoop();
            this._surface = new NullSurface();
        }
        #endregion

        #region Fields
        private IGraphicsInitializer _graphicsProvider;
        private ISoundInitializer _soundInitializer;

        private IFileSystem _fileSystem;
        private IGameLoop _gameLoop;
        private ISurface _surface;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to register the default components.
        /// </summary>
        public bool RegisterDefaultComponents { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the graphics initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void Set(IGraphicsInitializer initializer)
        {
            this._graphicsProvider = initializer;
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
        /// Sets the specified file system.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public void Set(IFileSystem fileSystem)
        {
            this._fileSystem = fileSystem;
        }
        /// <summary>
        /// Sets the specified game loop.
        /// </summary>
        /// <param name="gameLoop">The game loop.</param>
        public void Set(IGameLoop gameLoop)
        {
            this._gameLoop = gameLoop;
        }
        /// <summary>
        /// Sets the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        public void Set(ISurface surface)
        {
            this._surface = surface;
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
        public override IGraphicsInitializer GraphicsProvider
        {
            get { return this._graphicsProvider; }
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
        /// <summary>
        /// Gets the game loop.
        /// </summary>
        public override IGameLoop GameLoop
        {
            get { return this._gameLoop; }
        }
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public override ISurface Surface
        {
            get { return this._surface; }
        }
        #endregion
    }
}