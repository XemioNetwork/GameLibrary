using System.Collections.Generic;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary
{
    /// <summary>
    /// Works as a configuration for the XGL.
    /// </summary>
    public abstract class Configuration
    {
        #region Properties
        /// <summary>
        /// Gets the start scenes.
        /// </summary>
        public IList<Scene> Scenes { get; private set; } 
        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        public IList<IComponent> Components { get; private set; }
        /// <summary>
        /// Gets or sets the size of the render.
        /// </summary>
        public Vector2 BackBufferSize { get; set; }
        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        public int FrameRate { get; set; }
        /// <summary>
        /// Gets a value indicating whether to create a default player input.
        /// </summary>
        public bool CreatePlayerInput { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show splash screen.
        /// </summary>
        public bool SplashScreenEnabled { get; set; }
        /// <summary>
        /// Gets or sets the graphics initializer.
        /// </summary>
        public abstract IGraphicsInitializer GraphicsProvider { get; }
        /// <summary>
        /// Gets the file system.
        /// </summary>
        public abstract IFileSystem FileSystem { get; }
        /// <summary>
        /// Gets the game loop.
        /// </summary>
        public abstract IGameLoop GameLoop { get; }
        /// <summary>
        /// Gets the surface.
        /// </summary>
        public abstract ISurface Surface { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        protected Configuration()
        {
            this.Scenes = new List<Scene>();
            this.Components = new List<IComponent>();

            this.BackBufferSize = new Vector2(1280, 720);
            this.FrameRate = 60;

            this.SplashScreenEnabled = true;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Registers the start scenes.
        /// </summary>
        public virtual void RegisterScenes()
        {
        }
        /// <summary>
        /// Registers the default components.
        /// </summary>
        public virtual void RegisterComponents()
        {
            this.Components.Add(this.FileSystem);
            this.Components.Add(this.GameLoop);
            this.Components.Add(this.Surface);

            this.Components.Add(new EventManager());
            this.Components.Add(new SceneManager());
            this.Components.Add(new InputManager());
            this.Components.Add(new SerializationManager());
            this.Components.Add(new ImplementationManager());
            this.Components.Add(new ThreadInvoker());
            this.Components.Add(new LocalizationManager());
            this.Components.Add(new ApplicationExceptionHandler());
            this.Components.Add(new LibraryLoader());
        }
        #endregion Methods
    }
}
