﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Localization;
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
        /// Gets the component manager.
        /// </summary>
        public static ComponentManager Components
        {
            get { return ComponentManager.Instance; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Configuration"/> is initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Configures the XGL fluently.
        /// </summary>
        public static FluentConfigurator Configure()
        {
            return new FluentConfigurator();
        }
        /// <summary>
        /// Configures the XGL using the specified bootstrapper.
        /// </summary>
        /// <typeparam name="T">The type of the configuration.</typeparam>
        public static void Run<T>(IntPtr handle) where T : Configuration, new()
        {
            XGL.Run(handle, new T());
        }
        /// <summary>
        /// Configures the XGL using the specified bootstrapper.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="config">The configuration.</param>
        public static void Run(IntPtr handle, Configuration config)
        {
            if (XGL.Initialized)
                return;

            config.RegisterComponents();
            foreach (IComponent component in config.Components)
            {
                XGL.Components.Add(component);
            }

            XGL.InitializeGraphics(handle, config);
            XGL.InitializeSound(config);
            XGL.InitializeGameLoop(config);

            XGL.Components.Construct();

            config.RegisterStartScenes();

            var sceneManager = XGL.Components.Get<SceneManager>();
            sceneManager.Add(config.StartScenes);

            XGL.Initialized = true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="config">The bootstrapper.</param>
        private static void InitializeGraphics(IntPtr handle, Configuration config)
        {
            if (config.GraphicsInitializer != null && config.GraphicsInitializer.IsAvailable())
            {
                var graphicsDevice = new GraphicsDevice(handle);
                graphicsDevice.Graphics = config.GraphicsInitializer.CreateProvider(graphicsDevice);
                graphicsDevice.DisplayMode = new DisplayMode(config.BackBufferSize);

                XGL.Components.Add(graphicsDevice);
            }
        }
        /// <summary>
        /// Initializes the sound.
        /// </summary>
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
        private static void InitializeGameLoop(Configuration config)
        {
            var gameLoop = XGL.Components.Get<GameLoop>();
            if (gameLoop == null)
                return;

            gameLoop.TargetFrameTime = 1000 / (double)config.FrameRate;
            gameLoop.TargetTickTime = 1000 / (double)config.FrameRate;

            gameLoop.Run();
        }
        #endregion Private Methods
    }
}
