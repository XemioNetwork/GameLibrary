using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Content.Loading;
using Xemio.GameLibrary.Game.Handlers;
using Xemio.GameLibrary.Game.Scenes.Transitions;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class Scene : SceneContainer, IEnumerable<Scene>
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
            this.IsVisible = true;
            this.IsPaused = false;

            this.LoadingReport = new SceneLoadingReport();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the parent.
        /// </summary>
        public SceneContainer Parent { get; internal set; }
        /// <summary>
        /// Gets the loading report.
        /// </summary>
        public ILoadingHandler LoadingReport { get; protected internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Scene"/> is loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Scene"/> is loading.
        /// </summary>
        public bool IsLoading { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> is visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> is paused.
        /// </summary>
        public bool IsPaused { get; set; }
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
        protected SerializationManager Serializer
        {
            get { return XGL.Components.Require<SerializationManager>(); }
        }
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        protected ContentManager ContentManager
        {
            get { return XGL.Components.Require<ContentManager>(); }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        protected IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        protected IRenderFactory RenderFactory
        {
            get { return this.GraphicsDevice.RenderFactory; }
        }
        /// <summary>
        /// Gets the mouse listener.
        /// </summary>
        protected InputManager InputManager
        {
            get { return XGL.Components.Get<InputManager>(); }
        }
        /// <summary>
        /// Gets the local player input.
        /// </summary>
        protected PlayerInput Input
        {
            get { return this.InputManager.LocalInput; }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        protected SceneManager SceneManager
        {
            get { return XGL.Components.Get<SceneManager>(); }
        }
        #endregion
        
        #region Internal Methods
        /// <summary>
        /// Loads the content for the specified scene.
        /// </summary>
        internal void LoadContentIfNeeded()
        {
            if (!this.IsLoaded && !this.IsLoading)
            {
                logger.Info("Starting content loading for {0}.", this);

                this.IsLoading = true;
                Action action = () =>
                {
                    var loader = new BatchedContentLoader(this.LoadingReport);

                    this.LoadContent(loader);
                    loader.ExecuteBatchedActions();

                    this.IsLoaded = true;
                    this.IsLoading = false;

                    logger.Info("Content for {0} loaded.", this);
                };

                Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Transitions the scene to the specified one.
        /// </summary>
        /// <typeparam name="TTransition">The type of the transition.</typeparam>
        /// <param name="scene">The scene.</param>
        protected void TransitionTo<TTransition>(Scene scene) where TTransition : ITransition, new()
        {
            this.TransitionTo(scene, new TTransition());
        }
        /// <summary>
        /// Transitions the scene to the specified one.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="transition">The transition.</param>
        protected void TransitionTo(Scene scene, ITransition transition)
        {
            this.TransitionTo(new TransitionScene(transition, this, scene));
        }
        /// <summary>
        /// Transitions the scene to the specified one.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected void TransitionTo(Scene scene)
        {
            this.SceneManager.Add(scene);
            this.Remove();
        }
        /// <summary>
        /// Brings the scene to the front.
        /// </summary>
        public void BringToFront()
        {
            this.SceneManager.Remove(this);
            this.SceneManager.Add(this);
        }
        /// <summary>
        /// Removes this scene.
        /// </summary>
        public void Remove()
        {
            if (this.Parent != null)
            {
                this.Parent.Remove(this);
            }
        }
        /// <summary>
        /// Loads the scenes content including textures, brushes, fonts, pens etc.
        /// </summary>
        /// <param name="loader">The content loader.</param>
        public virtual void LoadContent(IContentLoader loader)
        {
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
        
        #region IEnumerable<Scene> Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Scene> GetEnumerator()
        {
            return this.Scenes.GetEnumerator();
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
