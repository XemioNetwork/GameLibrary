using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class SceneContainer
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainer"/> class.
        /// </summary>
        protected SceneContainer()
        {
            this.Scenes = new CachedList<Scene>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the scenes.
        /// </summary>
        protected CachedList<Scene> Scenes { get; private set; } 
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        protected GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        #endregion
        
        #region Implementation of SceneContainer
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            scene.Parent = this;
            logger.Debug("Adding scene {0}.", scene.GetType().Name);
            
            scene.OnEnter();

            this.Scenes.Add(scene);
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            scene.Parent = null;
            logger.Debug("Removing scene {0}.", scene.GetType().Name);

            scene.OnLeave();

            this.Scenes.Remove(scene);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Orders the scenes for game tick processing.
        /// </summary>
        private IEnumerable<Scene> OrderedTickScenes()
        {
            return this.Scenes.Where(scene => !scene.IsPaused)
                              .OrderBy(scene => scene.TickIndex);
        }
        /// <summary>
        /// Orders the scenes for the rendering process.
        /// </summary>
        private IEnumerable<Scene> OrderedRenderScenes()
        {
            return this.Scenes.Where(scene => scene.IsLoaded && scene.IsVisible)
                              .OrderBy(scene => scene.RenderIndex);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a scene.
        /// </summary>
        public T GetScene<T>() where T : Scene
        {
            return (T)this.GetScene(scene => scene is T);
        }
        /// <summary>
        /// Gets a scene.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public Scene GetScene(Predicate<Scene> predicate)
        {
            return this.Scenes.FirstOrDefault(scene => predicate(scene));
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            using (this.Scenes.StartCaching())
            {
                foreach (Scene scene in this.OrderedTickScenes())
                {
                    scene.LoadContentIfNeeded();
                    if (scene.IsLoaded)
                    {
                        scene.Tick(elapsed);
                    }
                }
            }
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public virtual void Render()
        {
            using (this.Scenes.StartCaching())
            {
                foreach (Scene scene in this.OrderedRenderScenes())
                {
                    scene.Render();
                }
            }
        }
        #endregion
    }
}
