using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Game;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Game
{
    public class SceneManager : IGameHandler, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        public SceneManager()
        {
            this._scenes = new List<Scene>();
        }
        #endregion

        #region Fields
        private List<Scene> _scenes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.GetComponent<GraphicsDevice>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            scene.Initialize();
            this._scenes.Add(scene);
        }
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(IEnumerable<Scene> scenes)
        {
            foreach (Scene scene in scenes)
            {
                this.Add(scene);
            }
        }
        /// <summary>
        /// Adds the specified scenes.
        /// </summary>
        /// <param name="scenes">The scenes.</param>
        public void Add(params Scene[] scenes)
        {
            this.Add((IEnumerable<Scene>)scenes);
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            this._scenes.Remove(scene);
        }
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
            return this._scenes.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Orders the scenes for game tick processing.
        /// </summary>
        private IList<Scene> OrderedTickScenes()
        {
            return this._scenes.OrderBy(scene => scene.TickIndex).ToList();
        }
        /// <summary>
        /// Orders the scenes for the rendering process.
        /// </summary>
        private IList<Scene> OrderedRenderScenes()
        {
            return this._scenes.OrderBy(scene => scene.RenderIndex).ToList();
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="elapsed">The elapsed.</param>
        private void Tick(Scene scene, float elapsed)
        {
            if (!scene.Loaded)
            {
                scene.InternalLoadContent();
            }

            scene.Tick(elapsed);
            foreach (Scene subScene in scene.Scenes)
            {
                this.Tick(subScene, elapsed);
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            IList<Scene> scenes = this.OrderedTickScenes();
            for (int i = 0; i < scenes.Count; i++)
            {
                this.Tick(scenes[i], elapsed);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="scene">The scene.</param>
        private void Render(Scene scene)
        {
            if (!scene.Loaded) return;

            this.GraphicsDevice.RenderManager.Tint(Color.White);
            this.GraphicsDevice.RenderManager.Offset(Vector2.Zero);

            scene.Render();
            foreach (Scene subScene in scene.Scenes)
            {
                this.Render(subScene);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public void Render()
        {
            IList<Scene> scenes = this.OrderedRenderScenes();
            for (int i = 0; i < scenes.Count; i++)
            {
                this.Render(scenes[i]);
            }

            this.GraphicsDevice.Present();
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            GameLoop loop = XGL.GetComponent<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion
    }
}
