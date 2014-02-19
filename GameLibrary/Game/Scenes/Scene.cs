﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class Scene : SceneProvider, IEnumerable<Scene>
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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

        #region Helper Properties
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        public SerializationManager Serializer
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager ContentManager
        {
            get { return XGL.Components.Require<ContentManager>(); }
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
        /// Gets the mouse listener.
        /// </summary>
        public InputManager InputManager
        {
            get { return XGL.Components.Get<InputManager>(); }
        }
        /// <summary>
        /// Gets the local player input.
        /// </summary>
        public PlayerInput Input
        {
            get { return this.InputManager.LocalInput; }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public SceneManager SceneManager
        {
            get { return XGL.Components.Get<SceneManager>(); }
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
                logger.Info("Loading content for {0}.", this.GetType().Name);

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
