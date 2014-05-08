using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes.Transitions
{
    internal class TransitionScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionScene" /> class.
        /// </summary>
        /// <param name="transition">The transition.</param>
        /// <param name="current">The current.</param>
        /// <param name="next">The next.</param>
        public TransitionScene(ITransition transition, Scene current, Scene next)
        {
            this.Transition = transition;

            this.Current = current;
            this.Next = next;
        }
        #endregion

        #region Fields
        private IRenderTarget _currentTarget;
        private IRenderTarget _nextTarget;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the transition.
        /// </summary>
        public ITransition Transition { get; private set; }
        /// <summary>
        /// Gets the current scene.
        /// </summary>
        public Scene Current { get; private set; }
        /// <summary>
        /// Gets the next scene.
        /// </summary>
        public Scene Next { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the render targets if needed.
        /// </summary>
        private void InitializeRenderTargetsIfNeeded()
        {
            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();

            this._currentTarget = this._currentTarget ?? graphicsDevice.Factory.CreateTarget(
                graphicsDevice.DisplayMode.Width,
                graphicsDevice.DisplayMode.Height);

            this._nextTarget = this._nextTarget ?? graphicsDevice.Factory.CreateTarget(
                graphicsDevice.DisplayMode.Width,
                graphicsDevice.DisplayMode.Height);
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

            this.Transition.Tick(elapsed);
            if (this.Transition.IsCompleted)
            {
                this.SceneManager.Add(this.Next);
                this.Remove();
            }
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public override void Render()
        {
            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();

            this.InitializeRenderTargetsIfNeeded();

            if (this.Current.IsLoaded)
            {
                using (graphicsDevice.RenderTo(this._currentTarget))
                {
                    graphicsDevice.Clear(Color.Transparent);
                    this.Current.Render();
                }
            }

            if (this.Next.IsLoaded)
            {
                using (graphicsDevice.RenderTo(this._nextTarget))
                {
                    graphicsDevice.Clear(Color.Transparent);
                    this.Next.Render();
                }
            }

            this.Transition.Render(this._currentTarget, this._nextTarget);
        }
        #endregion
    }
}
