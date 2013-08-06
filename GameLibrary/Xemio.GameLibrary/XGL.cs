using System;
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
        /// Gets or sets a value indicating whether this <see cref="Bootstrapper"/> is initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Configures the XGL using the specified bootstrapper.
        /// </summary>
        /// <typeparam name="T">The type of the bootstrapper.</typeparam>
        public static void Run<T>(IntPtr handle) where T : Bootstrapper, new()
        {
            T bootstrapper = new T();
            XGL.Run(handle, bootstrapper);
        }
        /// <summary>
        /// Configures the XGL using the specified bootstrapper.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public static void Run(IntPtr handle, Bootstrapper bootstrapper)
        {
            if (XGL.Initialized)
                return;

            bootstrapper.RegisterComponents();
            foreach (IComponent component in bootstrapper.Components)
            {
                XGL.Components.Add(component);
            }

            XGL.InitializeGraphics(handle, bootstrapper);
            XGL.InitializeSound(bootstrapper);
            XGL.InitializeGameLoop(bootstrapper);

            XGL.Components.Construct();

            bootstrapper.RegisterStartScenes();

            var sceneManager = XGL.Components.Get<SceneManager>();
            sceneManager.Add(bootstrapper.StartScenes);

            XGL.Initialized = true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the graphics.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="bootstrapper">The bootstrapper.</param>
        private static void InitializeGraphics(IntPtr handle, Bootstrapper bootstrapper)
        {
            if (bootstrapper.GraphicsInitializer != null && bootstrapper.GraphicsInitializer.IsAvailable())
            {
                var graphicsDevice = new GraphicsDevice(handle);
                graphicsDevice.Graphics = bootstrapper.GraphicsInitializer.CreateProvider(graphicsDevice);
                graphicsDevice.DisplayMode = new DisplayMode(bootstrapper.RenderSize);

                XGL.Components.Add(graphicsDevice);
            }
        }
        /// <summary>
        /// Initializes the sound.
        /// </summary>
        private static void InitializeSound(Bootstrapper bootstrapper)
        {
            if (bootstrapper.SoundInitializer != null)
            {
                var soundManager = new SoundManager
                {
                    Provider = bootstrapper.SoundInitializer.CreateProvider()
                };

                XGL.Components.Add(soundManager);
                XGL.Components.Add(new LoopManager());
            }
        }
        /// <summary>
        /// Initializes the game loop.
        /// </summary>
        private static void InitializeGameLoop(Bootstrapper bootstrapper)
        {
            var gameLoop = XGL.Components.Get<GameLoop>();
            if (gameLoop == null)
                return;

            gameLoop.TargetFrameTime = 1000 / (double)bootstrapper.FrameRate;
            gameLoop.TargetTickTime = 1000 / (double)bootstrapper.FrameRate;

            gameLoop.Run();
        }
        #endregion Private Methods
    }
}
