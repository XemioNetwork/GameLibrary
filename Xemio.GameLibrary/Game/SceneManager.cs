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
        /// Gets the scene.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public Scene GetScene(Func<Scene, bool> predicate)
        {
            return this._scenes.FirstOrDefault(predicate);
        }
        #endregion

        #region IGameHandler Member
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            for (int i = 0; i < this._scenes.Count; i++)
            {
                Scene scene = this._scenes[i];
                if (!scene.Loaded)
                {
                    scene.InternalLoadContent();
                }

                scene.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public void Render()
        {
            for (int i = 0; i < this._scenes.Count; i++)
            {
                Scene scene = this._scenes[i];
                if (!scene.Loaded) continue;

                this.GraphicsDevice.RenderManager.Tint(Color.White);
                this.GraphicsDevice.RenderManager.Offset(Vector2.Zero);

                scene.Render();
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
