﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Sound;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game
{
    public abstract class Scene : CachedSceneProvider, IEnumerable<Scene>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        protected Scene()
        {
            this._scenes = new List<Scene>();

            this.Visible = true;
            this.Paused = false;
        }
        #endregion

        #region Fields
        private readonly List<Scene> _scenes;
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
        public ISceneProvider Parent { get; internal set; }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content
        {
            get { return XGL.Components.Get<ContentManager>(); }
        }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Get<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory
        {
            get { return this.GraphicsDevice.TextureFactory; }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        public IGeometryProvider Geometry
        {
            get { return this.GraphicsDevice.Geometry; }
        }
        /// <summary>
        /// Gets the key listener.
        /// </summary>
        public KeyListener Keyboard
        {
            get { return XGL.Components.Get<KeyListener>(); }
        }
        /// <summary>
        /// Gets the mouse listener.
        /// </summary>
        public MouseListener Mouse
        {
            get { return XGL.Components.Get<MouseListener>(); }
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
        internal void InternalLoadContent()
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

        #region CachedSceneProvider Methods
        /// <summary>
        /// Called when a scene gets added.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected override void OnAddScene(Scene scene)
        {
            scene.InternalLoadContent();
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