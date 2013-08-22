using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input.Listeners;
using Xemio.GameLibrary.Localization;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Rendering.Surfaces;
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
        /// Gets the component manager.
        /// </summary>
        public static ComponentManager Components
        {
            get { return Singleton<ComponentManager>.Value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Configuration"/> is initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a fluent XGL configuration.
        /// </summary>
        public static FluentConfigurator Configure()
        {
            return new FluentConfigurator();
        }
        /// <summary>
        /// Starts the XGL with the specified configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Run<T>() where T : Configuration, new()
        {
            XGL.Run(new T());
        }
        /// <summary>
        /// Starts the XGL with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Run(Configuration configuration)
        {
            if (XGL.Initialized)
                return;
            
            configuration.RegisterComponents();
            foreach (IComponent component in configuration.Components)
            {
                XGL.Components.Add(component);
            }

            XGL.InitializeGraphics(configuration);
            XGL.InitializeSound(configuration);
            XGL.InitializeGameLoop(configuration);
            XGL.InitializeInput(configuration);

            XGL.Components.Construct();

            XGL.InitializeScenes(configuration);
            XGL.Initialized = true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        /// <param name="config">The configuration.</param>
        private static void InitializeGraphics(Configuration config)
        {
            if (config.GraphicsProvider != null)
            {
                if (!config.GraphicsProvider.IsAvailable())
                {
                    throw new InvalidOperationException(
                        "The selected graphics initializer is unavailable. Maybe your PC doesn't support the selected graphics engine.");
                }

                var graphicsDevice = new GraphicsDevice
                                         {
                                             DisplayMode = new DisplayMode(config.BackBufferSize)
                                         };

                IGraphicsInitializer provider = config.GraphicsProvider;
                graphicsDevice.Initialize(provider);

                XGL.Components.Add(graphicsDevice);
            }
        }
        /// <summary>
        /// Initializes the sound.
        /// </summary>
        /// <param name="config">The configuration.</param>
        private static void InitializeSound(Configuration config)
        {
            if (config.SoundInitializer != null)
            {
                var soundManager = new SoundManager
                                       {
                                           Provider = config.SoundInitializer.CreateProvider()
                                       };

                XGL.Components.Add(soundManager);
                XGL.Components.Add(new LoopManager());
            }
        }
        /// <summary>
        /// Initializes the game loop.
        /// </summary>
        /// <param name="config">The configuration.</param>
        private static void InitializeGameLoop(Configuration config)
        {
            var gameLoop = XGL.Components.Get<IGameLoop>();

            if (gameLoop != null)
            {
                gameLoop.TargetFrameTime = 1000 / (double)config.FrameRate;
                gameLoop.TargetTickTime = 1000 / (double)config.FrameRate;

                gameLoop.Run();
            }
        }
        /// <summary>
        /// Initializes the input.
        /// </summary>
        /// <param name="config">The configuration.</param>
        private static void InitializeInput(Configuration config)
        {
            var inputManager = XGL.Components.Get<InputManager>();

            if (inputManager != null && config.CreatePlayerInput)
            {
                PlayerInput playerInput = inputManager.CreateInput();
                inputManager.AddListener(new MouseListener(), playerInput.PlayerIndex);
                inputManager.AddListener(new KeyboardListener(), playerInput.PlayerIndex);
            }
        }
        /// <summary>
        /// Initializes the scenes.
        /// </summary>
        /// <param name="config">The config.</param>
        private static void InitializeScenes(Configuration config)
        {
            var sceneManager = XGL.Components.Get<SceneManager>();

            if (sceneManager != null)
            {
                config.RegisterScenes();
                if (config.ShowSplashScreen)
                {
                    SplashScreen splashScreen = new SplashScreen(config.Scenes);
                    sceneManager.Add(splashScreen);

                    return;
                }

                sceneManager.Add(config.Scenes);
            }
        }
        #endregion Private Methods
    }
}
