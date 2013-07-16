using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Sound.Loops;
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary
{
    public static class XGL
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="XGL"/> is initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        /// <summary>
        /// Gets the components.
        /// </summary>
        private static ComponentManager Components
        {
            get { return ComponentManager.Instance; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public static void Add(IComponent component)
        {
            XGL.Components.Add(component);
        }
        /// <summary>
        /// Gets a component by the specified type.
        /// </summary>
        public static T GetComponent<T>() where T : IComponent
        {
            if (!XGL.Initialized)
            {
                throw new InvalidOperationException(
                    "You have to initialize the game library before resolving a component.");
            }

            return XGL.Components.GetComponent<T>();
        }
        /// <summary>
        /// Initializes the graphics provider.
        /// </summary>
        public static void Initialize(IGraphicsInitializer initializer)
        {
            XGL.Components.Add(new ValueProvider<IGraphicsInitializer>(initializer));
        }
        /// <summary>
        /// Initializes the sound system.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public static void Initialize(ISoundInitializer initializer)
        {
            XGL.Components.Add(new ValueProvider<ISoundInitializer>(initializer));
        }
        /// <summary>
        /// Creates the graphics components.
        /// </summary>
        public static void CreateGraphics(IntPtr handle, int width, int height)
        {
            GraphicsDevice graphicsDevice = new GraphicsDevice(handle);
            XGL.Components.Add(graphicsDevice);

            IGraphicsInitializer graphicsInitializer = XGL.Components.GetComponent<IGraphicsInitializer>();
            if (graphicsInitializer == null)
            {
                throw new InvalidOperationException(
                    "You have to pass in a graphics initializer before creating graphics.");
            }

            if (graphicsInitializer.IsAvailable())
            {
                graphicsDevice.Graphics = graphicsInitializer.CreateProvider(graphicsDevice);
                graphicsDevice.DisplayMode = new DisplayMode(width, height);
            }

            XGL.Initialized = true;
        }
        /// <summary>
        /// Creates the sound components.
        /// </summary>
        public static void CreateSound()
        {
            SoundManager soundManager = new SoundManager();
            ISoundInitializer soundInitializer = XGL.Components.GetComponent<ISoundInitializer>();

            if (soundInitializer != null)
            {
                soundManager.Provider = soundInitializer.CreateProvider();

                XGL.Components.Add(soundManager);
                XGL.Components.Add(new LoopManager());
            }
        }
        /// <summary>
        /// Sets up core components and intializes the rendering pipeline for the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="targetFps">The target FPS.</param>
        public static void Run(IntPtr handle, int width, int height, int targetFps)
        {
            GameLoop loop = new GameLoop();
            loop.TargetFrameTime = 1000 / (double)targetFps;
            loop.TargetTickTime = 1000 / (double)targetFps;

            XGL.CreateGraphics(handle, width, height);
            
            XGL.Components.Add(loop);
            XGL.Components.Add(new EventManager());
            XGL.Components.Add(new SceneManager());
            XGL.Components.Add(new KeyListener(handle));
            XGL.Components.Add(new MouseListener(handle));
            XGL.Components.Add(new ContentManager());
            XGL.Components.Add(new ImplementationManager());

            XGL.CreateSound();

            XGL.Components.Add(new PackageHandler());
            XGL.Components.Construct();

            loop.Run();
        }
        #endregion
    }
}
