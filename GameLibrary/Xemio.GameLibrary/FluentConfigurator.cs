using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary
{
    public class FluentConfigurator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfigurator"/> class.
        /// </summary>
        public FluentConfigurator()
        {
            this._configuration = new FluentConfiguration();
        }
        #endregion

        #region Fields
        private readonly FluentConfiguration _configuration;
        #endregion
        
        #region Methods
        /// <summary>
        /// Disables the splashscreen.
        /// </summary>
        public FluentConfigurator DisableSplashScreen()
        {
            this._configuration.ShowSplashScreen = false;
            return this;
        }
        /// <summary>
        /// Registers the default components to the component registry.
        /// </summary>
        public FluentConfigurator DefaultComponents()
        {
            this._configuration.RegisterDefaultComponents = true;
            return this;
        }
        /// <summary>
        /// Creates a default player input.
        /// </summary>
        public FluentConfigurator DefaultInput()
        {
            this._configuration.CreatePlayerInput = true;
            return this;
        }
        /// <summary>
        /// Sets the graphics system.
        /// </summary>
        /// <typeparam name="T">The graphics initializer type.</typeparam>
        public FluentConfigurator Graphics<T>() where T : IGraphicsInitializer, new()
        {
            return this.Graphics(new T());
        }
        /// <summary>
        /// Sets the graphics system.
        /// </summary>
        /// <typeparam name="T">The graphics initializer type.</typeparam>
        /// <param name="smoothing">The smoothing mode.</param>
        /// <param name="interpolation">The interpolation mode.</param>
        public FluentConfigurator Graphics<T>(SmoothingMode smoothing, InterpolationMode interpolation) where T : IGraphicsInitializer, new()
        {
            return this.Graphics(new T(), smoothing, interpolation);
        }
        /// <summary>
        /// Sets the graphics system.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public FluentConfigurator Graphics(IGraphicsInitializer initializer)
        {
            return this.Graphics(initializer, SmoothingMode.AntiAliased, InterpolationMode.Bicubic);
        }
        /// <summary>
        /// Sets the graphics system.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <param name="smoothing">The smoothing mode.</param>
        /// <param name="interpolation">The interpolation mode.</param>
        public FluentConfigurator Graphics(IGraphicsInitializer initializer, SmoothingMode smoothing, InterpolationMode interpolation)
        {
            initializer.SmoothingMode = smoothing;
            initializer.InterpolationMode = interpolation;
            
            this._configuration.Set(initializer);

            return this;
        }
        /// <summary>
        /// Sets the sound system.
        /// </summary>
        /// <typeparam name="T">The sound initializer type.</typeparam>
        public FluentConfigurator Sound<T>() where T : ISoundInitializer, new()
        {
            return this.Sound(new T());
        }
        /// <summary>
        /// Sets the sound system.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public FluentConfigurator Sound(ISoundInitializer initializer)
        {
            this._configuration.Set(initializer);
            return this;
        }
        /// <summary>
        /// Sets the file system.
        /// </summary>
        /// <typeparam name="T">The file system type.</typeparam>
        public FluentConfigurator FileSystem<T>() where T : IFileSystem, new ()
        {
            return this.FileSystem(new T());
        }
        /// <summary>
        /// Sets the file system.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public FluentConfigurator FileSystem(IFileSystem fileSystem)
        {
            this._configuration.Set(fileSystem);
            return this;
        }
        /// <summary>
        /// Sets the frame rate.
        /// </summary>
        /// <param name="frameRate">The frame rate.</param>
        public FluentConfigurator FrameRate(int frameRate)
        {
            this._configuration.FrameRate = frameRate;
            return this;
        }
        /// <summary>
        /// Adds the specified components to the component registry.
        /// </summary>
        /// <param name="components">The components.</param>
        public FluentConfigurator Components(params IComponent[] components)
        {
            foreach (IComponent component in components)
            {
                this._configuration.Components.Add(component);
            }

            return this;
        }
        /// <summary>
        /// Adds the specified components to the component registry.
        /// </summary>
        /// <param name="componentFunction">The component function.</param>
        public FluentConfigurator Components(Func<IEnumerable<IComponent>> componentFunction)
        {
            return this.Components(componentFunction().ToArray());
        }
        /// <summary>
        /// Adds the specified scenes to the scene manager.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public FluentConfigurator Scenes(params Scene[] scenes)
        {
            foreach (Scene scene in scenes)
            {
                this._configuration.Scenes.Add(scene);
            }

            return this;
        }
        /// <summary>
        /// Adds the specified scenes to the scene manager.
        /// </summary>
        /// <param name="scenesFunction">The scenes function.</param>
        public FluentConfigurator Scenes(Func<IEnumerable<Scene>> scenesFunction)
        {
            return this.Scenes(scenesFunction().ToArray());
        }
        /// <summary>
        /// Sets the backbuffer size to the specified value.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public FluentConfigurator BackBuffer(int width, int height)
        {
            this._configuration.BackBufferSize = new Vector2(width, height);
            return this;
        }
        /// <summary>
        /// Sets the backbuffer size to the specified value.
        /// </summary>
        /// <param name="size">The size.</param>
        public FluentConfigurator BackBuffer(Size size)
        {
            this._configuration.BackBufferSize = new Vector2(size.Width, size.Height);
            return this;
        }
        /// <summary>
        /// Sets the backbuffer size to the specified value.
        /// </summary>
        /// <param name="size">The size.</param>
        public FluentConfigurator BackBuffer(Vector2 size)
        {
            this._configuration.BackBufferSize = size;
            return this;
        }
        /// <summary>
        /// Builds the configuration.
        /// </summary>
        public Configuration BuildConfiguration()
        {
            return this._configuration;
        }
        #endregion
    }
}
