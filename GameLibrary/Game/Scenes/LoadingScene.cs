using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Loading;

namespace Xemio.GameLibrary.Game.Scenes
{
    public abstract class LoadingScene : Scene, ILoadingHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingScene"/> class.
        /// </summary>
        /// <param assetName="scene">The scene.</param>
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
        public int ElementCount { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }
        /// <summary>
        /// Called when an element is loading.
        /// </summary>
        /// <param name="assetName">The assetName.</param>
        public virtual IDisposable OnLoading(string assetName)
        {
            this.CurrentElement = assetName;
            return new ActionDisposable(() => this.CompletedElements.Add(assetName));
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
