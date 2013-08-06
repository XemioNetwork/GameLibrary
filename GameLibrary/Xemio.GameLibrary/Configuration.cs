using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Sound.Loops;

namespace Xemio.GameLibrary
{
    /// <summary>
    /// Works as a configuration for the XGL.
    /// Subclass it and configure everything.
    /// </summary>
    public abstract class Configuration
    {
        #region Properties
        /// <summary>
        /// Gets the start scenes.
        /// </summary>
        public IList<Scene> StartScenes { get; private set; } 
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
        /// Gets or sets the graphics initializer.
        /// </summary>
        public abstract IGraphicsInitializer GraphicsInitializer { get; }
        /// <summary>
        /// Gets or sets the sound initializer.
        /// </summary>
        public abstract ISoundInitializer SoundInitializer { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        protected Configuration()
        {
            this.StartScenes = new List<Scene>();
            this.Components = new List<IComponent>();

            this.BackBufferSize = new Vector2(1280, 720);
            this.FrameRate = 60;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Registers the start scenes.
        /// </summary>
        public virtual void RegisterStartScenes()
        {
        }
        /// <summary>
        /// Registers the default components.
        /// </summary>
        public virtual void RegisterComponents()
        {
            this.Components.Add(new GameLoop());
            this.Components.Add(new EventManager());
            this.Components.Add(new SceneManager());
            this.Components.Add(new KeyListener());
            this.Components.Add(new MouseListener());
            this.Components.Add(new ContentManager());
            this.Components.Add(new ImplementationManager());
            this.Components.Add(new ThreadInvoker());
            this.Components.Add(new LocalizationManager());
            this.Components.Add(new GlobalExceptionHandler());
        }
        #endregion Methods
    }
}
