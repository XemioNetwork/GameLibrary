using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xemio.GameLibrary.Config.Installation;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Config
{
    public static class FluentExtensions
    {
        #region Access Methods
        /// <summary>
        /// Configurations the configuration from the specified access.
        /// </summary>
        /// <param name="access">The access.</param>
        internal static Configuration GetConfiguration(this IConfigurationAccess access)
        {
            return access.Configuration;
        }
        #endregion

        #region Graphics Methods
        /// <summary>
        /// Creates a graphics device and responsive graphics components.
        /// </summary>
        /// <typeparam name="T">The graphics initializer type.</typeparam>
        /// <param name="fluent">The fluent.</param>
        public static FluentConfiguration Graphics<T>(this FluentConfiguration fluent) where T : IGraphicsInitializer
        {
            fluent.Graphics(typeof(T));
            return fluent;
        }
        /// <summary>
        /// Creates a graphics device and responsive graphics components.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="initializerType">Type of the initializer.</param>
        public static FluentConfiguration Graphics(this FluentConfiguration fluent, Type initializerType)
        {
            fluent.Graphics((IGraphicsInitializer)Activator.CreateInstance(initializerType));
            return fluent;
        }
        /// <summary>
        /// Creates a graphics device and responsive graphics components.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="initializer">The initializer.</param>
        public static FluentConfiguration Graphics(this FluentConfiguration fluent, IGraphicsInitializer initializer)
        {
            var graphicsInstaller = fluent.GetConfiguration().GetOrInstall<GraphicsInstaller>();
            graphicsInstaller.Initializer = initializer;

            return fluent;
        }
        /// <summary>
        /// Creates a graphics device and responsive graphics components and sets the smoothing mode to the specified value.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="smoothingMode">The smoothing mode.</param>
        public static FluentConfiguration SmoothingMode(this FluentConfiguration fluent, SmoothingMode smoothingMode)
        {
            var graphicsInstaller = fluent.GetConfiguration().GetOrInstall<GraphicsInstaller>();
            graphicsInstaller.SmoothingMode = smoothingMode;

            return fluent;
        }
        /// <summary>
        /// Creates a graphics device and responsive graphics components and sets the interpolation mode to the specified value.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="interpolationMode">The interpolation mode.</param>
        public static FluentConfiguration InterpolationMode(this FluentConfiguration fluent, InterpolationMode interpolationMode)
        {
            var graphicsInstaller = fluent.GetConfiguration().GetOrInstall<GraphicsInstaller>();
            graphicsInstaller.InterpolationMode = interpolationMode;

            return fluent;
        }
        /// <summary>
        /// Sets the back buffer size.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static FluentConfiguration BackBuffer(this FluentConfiguration fluent, float width, float height)
        {
            fluent.BackBuffer(new Vector2(width, height));
            return fluent;
        }
        /// <summary>
        /// Sets the back buffer size.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="size">The size.</param>
        public static FluentConfiguration BackBuffer(this FluentConfiguration fluent, Size size)
        {
            fluent.BackBuffer(size.Width, size.Height);
            return fluent;
        }
        /// <summary>
        /// Sets the back buffer size.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="size">The size.</param>
        public static FluentConfiguration BackBuffer(this FluentConfiguration fluent, Vector2 size)
        {
            var graphicsInstaller = fluent.GetConfiguration().GetOrInstall<GraphicsInstaller>();
            graphicsInstaller.BackBufferSize = size;

            return fluent;
        }
        /// <summary>
        /// Sets the surface that is used to be rendered on.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="surface">The surface.</param>
        public static FluentConfiguration Surface(this FluentConfiguration fluent, ISurface surface)
        {
            var surfaceInstaller = fluent.GetConfiguration().GetOrInstall<SurfaceInstaller>();
            surfaceInstaller.Surface = surface;

            return fluent;
        }
        /// <summary>
        /// Sets the control that is used to be rendered on.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="control">The control.</param>
        public static FluentConfiguration Surface(this FluentConfiguration fluent, Control control)
        {
            var surfaceInstaller = fluent.GetConfiguration().GetOrInstall<SurfaceInstaller>();
            surfaceInstaller.Surface = new WindowSurface(control.Handle);

            return fluent;
        }
        #endregion

        #region Content Methods
        /// <summary>
        /// Sets the content trackingEnabled enabled or disabled.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="cacheEnabled">if set to <c>true</c> enables the content cache.</param>
        public static FluentConfiguration ContentCache(this FluentConfiguration fluent, bool cacheEnabled)
        {
            var contentInstaller = fluent.GetConfiguration().GetOrInstall<ContentInstaller>();
            contentInstaller.IsCachingEnabled = cacheEnabled;

            return fluent;
        }
        /// <summary>
        /// Sets the content tracking enabled or disabled.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="trackingEnabled">The tracking.</param>
        public static FluentConfiguration ContentTracking(this FluentConfiguration fluent, bool trackingEnabled)
        {
            var contentInstaller = fluent.GetConfiguration().GetOrInstall<ContentInstaller>();
            contentInstaller.IsTrackingEnabled = trackingEnabled;

            return fluent;
        }
        /// <summary>
        /// Sets the content format to the specified value. All content will be serialized and deserialized using the specified format.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="format">The format.</param>
        public static FluentConfiguration ContentFormat(this FluentConfiguration fluent, IFormat format)
        {
            var contentInstaller = fluent.GetConfiguration().GetOrInstall<ContentInstaller>();
            contentInstaller.Format = format;

            return fluent;
        }
        /// <summary>
        /// Sets the file system.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="fileSystem">The file system.</param>
        public static FluentConfiguration FileSystem(this FluentConfiguration fluent, IFileSystem fileSystem)
        {
            var fileSystemInstaller = fluent.GetConfiguration().GetOrInstall<FileSystemInstaller>();
            fileSystemInstaller.FileSystem = fileSystem;

            return fluent;
        }
        #endregion

        #region Input Methods
        /// <summary>
        /// Disables the creation of a default player input.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        public static FluentConfiguration CreatePlayerInput(this FluentConfiguration fluent)
        {
            var inputInstaller = fluent.GetConfiguration().GetOrInstall<InputInstaller>();
            inputInstaller.CreatePlayerInput = true;

            return fluent;
        }
        #endregion

        #region Scene Methods
        /// <summary>
        /// Adds the specified scene to the scene manager.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="scene">The scene.</param>
        public static FluentConfiguration Scene(this FluentConfiguration fluent, Scene scene)
        {
            var sceneInstaller = fluent.GetConfiguration().GetOrInstall<SceneInstaller>();
            sceneInstaller.Scenes.Add(scene);

            return fluent;
        }
        /// <summary>
        /// Adds the specified scenes to the scene manager.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="scenesFunction">The scenes function.</param>
        public static FluentConfiguration Scenes(this FluentConfiguration fluent, Func<IEnumerable<Scene>> scenesFunction)
        {
            fluent.Scenes(scenesFunction());
            return fluent;
        }
        /// <summary>
        /// Adds the specified scenes to the scene manager.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="scenes">The scenes.</param>
        public static FluentConfiguration Scenes(this FluentConfiguration fluent, IEnumerable<Scene> scenes)
        {
            var sceneInstaller = fluent.GetConfiguration().GetOrInstall<SceneInstaller>();
            sceneInstaller.Scenes.AddRange(scenes);

            return fluent;
        }
        /// <summary>
        /// Disables the splash screen.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        public static FluentConfiguration DisableSplashScreen(this FluentConfiguration fluent)
        {
            var sceneInstaller = fluent.GetConfiguration().GetOrInstall<SceneInstaller>();
            sceneInstaller.SplashScreenEnabled = false;

            return fluent;
        }
        #endregion

        #region Script Methods
        /// <summary>
        /// Sets the script directory.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="directory">The directory.</param>
        public static FluentConfiguration ScriptDirectory(this FluentConfiguration fluent, string directory)
        {
            var scriptInstaller = fluent.GetConfiguration().GetOrInstall<ScriptInstaller>();
            scriptInstaller.RootDirectory = directory;

            return fluent;
        }
        #endregion

        #region GameLoop Methods
        /// <summary>
        /// Sets the frames per second.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="value">The value.</param>
        public static FluentConfiguration FramesPerSecond(this FluentConfiguration fluent, int value)
        {
            var gameLoopInstaller = fluent.GetConfiguration().GetOrInstall<GameLoopInstaller>();
            gameLoopInstaller.TargetFrameTime = 1000 / (float)value;

            return fluent;
        }
        /// <summary>
        /// Sets the game loop lag compensation.
        /// </summary>
        /// <param name="fluent">The fluent.</param>
        /// <param name="lagCompensation">The lag compensation.</param>
        public static FluentConfiguration LagCompensation(this FluentConfiguration fluent, LagCompensation lagCompensation)
        {
            var gameLoopInstaller = fluent.GetConfiguration().GetOrInstall<GameLoopInstaller>();
            gameLoopInstaller.LagCompensation = lagCompensation;

            return fluent;
        }
        #endregion
    }
}
