using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class LoadingScene : Scene, ILoadingReport
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingScene"/> class.
        /// </summary>
        /// <param name="scene">The scene.</param>
        protected LoadingScene(Scene scene)
        {
            this.Target = scene;
            this.Target.LoadingReport = this;

            this.CompletedElements = new List<string>();

            this.Add(this.Target);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether loading the content is completed.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Gets the current element.
        /// </summary>
        public string CurrentElement { get; private set; }
        /// <summary>
        /// Gets the completed elements.
        /// </summary>
        public IList<string> CompletedElements { get; private set; } 
        /// <summary>
        /// Gets the scene.
        /// </summary>
        public Scene Target { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the scene is loaded.
        /// </summary>
        protected virtual void OnCompleted()
        {
            this.TransitionTo(this.Target);
        }
        #endregion

        #region Implementation of ILoadingReport
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public int Elements { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }
        /// <summary>
        /// Called when an element is loading.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void OnLoading(string name)
        {
            this.CurrentElement = name;
        }
        /// <summary>
        /// Called when a file was loaded.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void OnLoaded(string name)
        {
            this.CompletedElements.Add(name);
        }
        #endregion

        #region Overrides of SceneContainer
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);

            if (this.Target.IsLoaded && !this.IsCompleted)
            {
                this.IsCompleted = true;
                this.OnCompleted();
            }
        }
        #endregion
    }
}
