using System.Collections.Generic;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Rendering.Surfaces;
using Xemio.GameLibrary.Script;

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
        /// Gets or sets the content format.
        /// </summary>
        public IFormat ContentFormat { get; set; }
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
            this.ContentFormat = Format.Binary;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Enables the core components.
        /// </summary>
        protected void EnableCoreComponents()
        {
            this.EnableCommons();
            this.EnableContentSystem();
            this.EnablePluginSystem();
            this.EnableInputSystem();
            this.EnableLocalizationSystem();
            this.EnableScriptSystem();
            this.EnableGameLoop();
            this.EnableSurface();
        }
        /// <summary>
        /// Enables the commons.
        /// </summary>
        protected virtual void EnableCommons()
        {
            this.Components.Add(new EventManager());
            this.Components.Add(new ThreadInvoker());
            this.Components.Add(new ApplicationExceptionHandler());
        }
        /// <summary>
        /// Enables the content system.
        /// </summary>
        protected virtual void EnableContentSystem()
        {
            if (this.FileSystem != null)
            {
                this.Components.Add(this.FileSystem);
            }

            this.Components.Add(new SerializationManager());
            this.Components.Add(new ContentManager());
        }
        /// <summary>
        /// Enables the plugin system.
        /// </summary>
        protected virtual void EnablePluginSystem()
        {
            this.Components.Add(new ImplementationManager());
            this.Components.Add(new LibraryLoader());
        }
        /// <summary>
        /// Enables the input system.
        /// </summary>
        protected virtual void EnableInputSystem()
        {
            this.Components.Add(new InputManager());
        }
        /// <summary>
        /// Enables the localization system.
        /// </summary>
        protected virtual void EnableLocalizationSystem()
        {
            this.Components.Add(new LocalizationManager());
        }
        /// <summary>
        /// Enables the script system.
        /// </summary>
        protected virtual void EnableScriptSystem()
        {
            this.Components.Add(new ScriptExecutor());
            this.Components.Add(new DynamicScriptLoader());
        }
        /// <summary>
        /// Enables the game loop.
        /// </summary>
        protected virtual void EnableGameLoop()
        {
            if (this.GameLoop != null)
            {
                this.Components.Add(this.GameLoop);
                this.Components.Add(new SceneManager());
            }
        }
        /// <summary>
        /// Enables the surface.
        /// </summary>
        protected virtual void EnableSurface()
        {
            if (this.Surface != null)
            {
                this.Components.Add(this.Surface);
            }
        }
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
            this.EnableCoreComponents();
        }
        #endregion Methods
    }
}
