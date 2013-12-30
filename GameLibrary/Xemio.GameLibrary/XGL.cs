using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NLog;
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
using Xemio.GameLibrary.Network;
using Xemio.GameLibrary.Network.Packages;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary
{
    public static class XGL
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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
        public static XGLState State { get; internal set; }
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
            if (XGL.State == XGLState.Initialized)
            {
                logger.Warn("XGL has already been configured.");
                return;
            }

            XGL.State = XGLState.Initializing;
            
            logger.Info("Preparing for configuration.");

            configuration.RegisterComponents();
            foreach (IComponent component in configuration.Components)
            {
                XGL.Components.Add(component);
            }

            XGL.Initialize(configuration);
            XGL.Components.Construct();

            XGL.InitializeScenes(configuration);
            XGL.State = XGLState.Initialized;

            var gameLoop = XGL.Components.Get<IGameLoop>();
            if (gameLoop != null)
            {
                gameLoop.Run();
            }

            logger.Info("Done initializing.");
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void Initialize(Configuration configuration)
        {
            XGL.InitializeContent(configuration);
            XGL.InitializeGraphics(configuration);
            XGL.InitializeGameLoop(configuration);
            XGL.InitializeInput(configuration);
        }
        /// <summary>
        /// Initializes the content.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void InitializeContent(Configuration configuration)
        {
            var content = new ContentManager();
            content.Format = configuration.ContentFormat;

            XGL.Components.Add(content);
        }
        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void InitializeGraphics(Configuration configuration)
        {
            if (configuration.GraphicsProvider != null)
            {
                logger.Info("Initializing graphics with {0}.", configuration.GraphicsProvider.GetType().Name);

                if (!configuration.GraphicsProvider.IsAvailable())
                {
                    throw new InvalidOperationException(
                        "The selected graphics initializer is unavailable. Maybe your PC doesn't support the selected graphics engine.");
                }

                logger.Info("Setting display mode to {0}x{1}.", (int)configuration.BackBufferSize.X, (int)configuration.BackBufferSize.Y);

                var graphicsDevice = new GraphicsDevice
                {
                    DisplayMode = new DisplayMode(configuration.BackBufferSize)
                };

                graphicsDevice.Initialize(configuration.GraphicsProvider);

                XGL.Components.Add(graphicsDevice);
            }
        }
        /// <summary>
        /// Initializes the game loop.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void InitializeGameLoop(Configuration configuration)
        {
            if (configuration.GameLoop != null)
            {
                logger.Info("Initializing game loop with {0}fps", configuration.FrameRate);

                configuration.GameLoop.TargetFrameTime = 1000 / (double)configuration.FrameRate;
            }
        }
        /// <summary>
        /// Initializes the input.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void InitializeInput(Configuration configuration)
        {
            var inputManager = XGL.Components.Get<InputManager>();

            if (inputManager != null && configuration.CreatePlayerInput)
            {
                PlayerInput playerInput = inputManager.CreateInput();

                inputManager.AddListener(new MouseListener(), playerInput.PlayerIndex);
                inputManager.AddListener(new KeyboardListener(), playerInput.PlayerIndex);
            }
        }
        /// <summary>
        /// Initializes the scenes.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void InitializeScenes(Configuration configuration)
        {
            var sceneManager = XGL.Components.Get<SceneManager>();

            if (sceneManager != null)
            {
                logger.Info("Initializing scenes. Count: ", configuration.Scenes.Count);

                configuration.RegisterScenes();
                if (configuration.SplashScreenEnabled)
                {
                    logger.Info("Splash screen is enabled. Creating splash screen.");

                    var splashScreen = new SplashScreen(configuration.Scenes);
                    sceneManager.Add(splashScreen);

                    return;
                }

                sceneManager.Add(configuration.Scenes);
            }
        }
        #endregion
    }
}
