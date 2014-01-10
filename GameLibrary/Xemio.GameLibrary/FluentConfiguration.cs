using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Rendering.Surfaces;
using Xemio.GameLibrary.Script;

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
            this._gameLoop = new GameLoop();
            this._fileSystem = new DiskFileSystem();
            this._surface = new NullSurface();
        }
        #endregion

        #region Fields
        private IGraphicsInitializer _graphicsProvider;

        private IFileSystem _fileSystem;
        private IGameLoop _gameLoop;
        private ISurface _surface;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to register the default components.
        /// </summary>
        public bool CoreComponentsEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the input system is enabled.
        /// </summary>
        public bool InputEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the game loop and scene manager are enabled.
        /// </summary>
        public bool GameLoopEnabled { get; set; }
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
        /// <summary>
        /// Registers the default components.
        /// </summary>
        public override void RegisterComponents()
        {
            if (this.CoreComponentsEnabled)
            {
                this.EnableCoreComponents();
            }
        }
        /// <summary>
        /// Enables the game loop.
        /// </summary>
        protected override void EnableGameLoop()
        {
            if (this.GameLoopEnabled)
            {
                base.EnableGameLoop();
            }
        }
        /// <summary>
        /// Enables the input system.
        /// </summary>
        protected override void EnableInputSystem()
        {
            if (this.InputEnabled)
            {
                base.EnableInputSystem();
            }
        }
        #endregion
    }
}