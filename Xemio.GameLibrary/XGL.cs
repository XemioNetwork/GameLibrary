using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Sound.Loops;
using Xemio.GameLibrary.Network;

namespace Xemio.GameLibrary
{
    public static class XGL
    {
        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        public static ComponentManager Components
        {
            get { return ComponentManager.Instance; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a component by the specified type.
        /// </summary>
        public static T GetComponent<T>() where T : IComponent
        {
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
        /// Sets up core components and intializes the rendering pipeline for the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void Run(IntPtr handle, int width, int height, int targetFps)
        {
            GameLoop loop = new GameLoop();
            loop.TargetFrameTime = 1000 / (double)targetFps;
            loop.TargetTickTime = 1000 / (double)targetFps;

            GraphicsDevice graphicsDevice = new GraphicsDevice(handle);
            IGraphicsInitializer initializer = XGL.GetComponent<IGraphicsInitializer>();

            graphicsDevice.Graphics = initializer.CreateProvider(graphicsDevice);
            graphicsDevice.SetDisplayMode(width, height);
            
            XGL.Components.Add(loop);
            XGL.Components.Add(graphicsDevice);
            XGL.Components.Add(new EventManager());
            XGL.Components.Add(new SceneManager());
            XGL.Components.Add(new KeyListener(handle));
            XGL.Components.Add(new MouseListener(handle));
            XGL.Components.Add(new SoundManager());
            XGL.Components.Add(new LoopManager());
            XGL.Components.Add(new PackageHandler());

            XGL.Components.Construct();
            loop.Run();
        }
        #endregion
    }
}
