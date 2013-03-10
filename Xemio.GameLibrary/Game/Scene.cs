using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary.Game
{
    public abstract class Scene : ISceneProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this.Scenes = new List<Scene>();
        }
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
        /// <summary>
        /// Gets the scenes.
        /// </summary>
        public List<Scene> Scenes { get; private set; }
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
            return this.Scenes.FirstOrDefault(predicate);
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

        #region ISceneProvider Member
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            scene.Parent = this;
            scene.Initialize();

            this.Scenes.Add(scene);
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            scene.Parent = null;
            this.Scenes.Remove(scene);
        }
        #endregion
    }
}
