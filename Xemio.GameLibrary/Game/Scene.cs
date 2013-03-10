using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary.Game
{
    public abstract class Scene : CachedSceneProvider, IEnumerable<Scene>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this._scenes = new List<Scene>();
        }
        #endregion

        #region Fields
        private List<Scene> _scenes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Scene"/> is loaded.
        /// </summary>
        public bool Loaded { get; private set; }
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public ISceneProvider Parent { get; internal set; }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.GetComponent<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the key listener.
        /// </summary>
        public KeyListener Keyboard
        {
            get { return XGL.GetComponent<KeyListener>(); }
        }
        /// <summary>
        /// Gets the mouse listener.
        /// </summary>
        public MouseListener Mouse
        {
            get { return XGL.GetComponent<MouseListener>(); }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager
        {
            get { return XGL.GetComponent<SceneManager>(); }
        }
        /// <summary>
        /// Gets the sound manager.
        /// </summary>
        public SoundManager SoundManager
        {
            get { return XGL.GetComponent<SoundManager>(); }
        }
        /// <summary>
        /// Gets a value indicating the index during a game tick.
        /// </summary>
        public virtual int TickIndex
        {
            get { return 0; }
        }
        /// <summary>
        /// Gets a value indicating the index during the rendering process.
        /// </summary>
        public virtual int RenderIndex
        {
            get { return 0; }
        }
        #endregion

        #region Scene Methods
        /// <summary>
        /// Brings the scene to the front.
        /// </summary>
        public void BringToFront()
        {
            this.SceneManager.Remove(this);
            this.SceneManager.Add(this);
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content for the specified scene.
        /// </summary>
        internal void InternalLoadContent()
        {
            this.LoadContent();
            this.Loaded = true;
        }
        /// <summary>
        /// Removes this scene.
        /// </summary>
        public void Remove()
        {
            this.Parent.Remove(this);
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
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Initializes the scene.
        /// </summary>
        public virtual void Initialize()
        {
        }
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public virtual void LoadContent()
        {
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public virtual void Render()
        {
        }
        #endregion

        #region CachedSceneProvider Methods
        /// <summary>
        /// Called when a scene gets added.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected override void OnAddScene(Scene scene)
        {
            this._scenes.Add(scene);
        }
        /// <summary>
        /// Called when a scene gets removed.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected override void OnRemoveScene(Scene scene)
        {
            this._scenes.Remove(scene);
        }
        #endregion

        #region IEnumerable<Scene> Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Scene> GetEnumerator()
        {
            return this._scenes.GetEnumerator();
        }
        #endregion

        #region IEnumerable Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
