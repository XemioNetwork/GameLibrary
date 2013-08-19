using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class Scene : SceneProvider, IEnumerable<Scene>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        protected Scene()
        {
            this.Visible = true;
            this.Paused = false;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Scene"/> is loaded.
        /// </summary>
        public bool Loaded { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> is paused.
        /// </summary>
        public bool Paused { get; set; }
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public SceneProvider Parent { get; internal set; }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content
        {
            get { return XGL.Components.Get<ContentManager>(); }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory RenderFactory
        {
            get { return this.GraphicsDevice.RenderFactory; }
        }
        /// <summary>
        /// Gets the geometry manager.
        /// </summary>
        public IGeometryManager GeometryManager
        {
            get { return this.GraphicsDevice.GeometryManager; }
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        public IGeometryFactory GeometryFactory
        {
            get { return this.GraphicsDevice.GeometryFactory; }
        }
        /// <summary>
        /// Gets the mouse listener.
        /// </summary>
        public InputManager Input
        {
            get { return XGL.Components.Get<InputManager>(); }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager
        {
            get { return XGL.Components.Get<SceneManager>(); }
        }
        /// <summary>
        /// Gets the sound manager.
        /// </summary>
        public SoundManager SoundManager
        {
            get { return XGL.Components.Get<SoundManager>(); }
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
        internal void TryLoadContent()
        {
            if (!this.Loaded)
            {
                this.LoadContent();
                this.Loaded = true;
            }
        }
        /// <summary>
        /// Removes this scene.
        /// </summary>
        public void Remove()
        {
            this.Parent.Remove(this);
        }
        /// <summary>
        /// Called when the scene was added to a parent.
        /// </summary>
        public virtual void OnEnter()
        {
            
        }
        /// <summary>
        /// Called when the scene gets removed from it's parent.
        /// </summary>
        public virtual void OnLeave()
        {
            
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public virtual void LoadContent()
        {
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Wraps the specified game handler.
        /// </summary>
        /// <param name="gameHandler">The game handler.</param>
        /// <returns></returns>
        public static Scene Wrap(IGameHandler gameHandler)
        {
            return new SceneWrapper(gameHandler);    
        }
        #endregion
        
        #region IEnumerable<Scene> Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Scene> GetEnumerator()
        {
            return this._subScenes.GetEnumerator();
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
