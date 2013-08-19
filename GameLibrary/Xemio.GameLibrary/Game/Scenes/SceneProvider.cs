using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common.Collections;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes
{
    [Require(typeof(GraphicsDevice))]

    public abstract class SceneProvider
    {
        #region Fields
        protected readonly CachedList<Scene> _subScenes = new CachedList<Scene>();
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Get<GraphicsDevice>(); }
        }
        #endregion Properties
        
        #region Implementation of ISceneProvider
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            scene.Parent = this;
            scene.OnEnter();

            this._subScenes.Add(scene);
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            scene.Parent = null;
            scene.OnLeave();

            this._subScenes.Remove(scene);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Orders the scenes for game tick processing.
        /// </summary>
        private IEnumerable<Scene> OrderedTickScenes()
        {
            return this._subScenes.OrderBy(scene => scene.TickIndex);
        }
        /// <summary>
        /// Orders the scenes for the rendering process.
        /// </summary>
        private IEnumerable<Scene> OrderedRenderScenes()
        {
            return this._subScenes.OrderBy(scene => scene.RenderIndex);
        }
        #endregion Private Methods

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
        public Scene GetScene(Func<Scene, bool> predicate)
        {
            return this._subScenes.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            using (this._subScenes.StartCaching())
            {
                foreach (Scene scene in this.OrderedTickScenes())
                {
                    if (scene.Paused)
                        continue;

                    scene.TryLoadContent();
                    scene.Tick(elapsed);
                }
            }
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public virtual void Render()
        {
            using (this._subScenes.StartCaching())
            {
                foreach (Scene scene in this.OrderedRenderScenes())
                {
                    if (!scene.Visible || !scene.Loaded)
                        continue;

                    if (this.GraphicsDevice != null)
                        this.GraphicsDevice.RenderManager.Translate(Vector2.Zero);

                    scene.Render();
                }
            }
        }
        #endregion Methods
    }
}
