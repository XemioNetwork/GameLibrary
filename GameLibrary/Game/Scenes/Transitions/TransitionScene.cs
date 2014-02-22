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
        /// Initializes a new instance of the <see cref="TransitionScene"/> class.
        /// </summary>
        /// <param name="transition">The transition.</param>
        public TransitionScene(ITransition transition)
        {
            this.Transition = transition;
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
        #endregion

        #region Overrides of SceneContainer
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Transition.Current.Tick(elapsed);
            this.Transition.Next.Tick(elapsed);

            this.Transition.Tick(elapsed);
            if (this.Transition.IsCompleted)
            {
                this.SceneManager.Add(this.Transition.Next);
                this.Remove();
            }
        }
        /// <summary>
        /// Handles a game render.
        /// </summary>
        public override void Render()
        {
            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();

            this._currentTarget = this._currentTarget ?? graphicsDevice.RenderFactory.CreateTarget(
                graphicsDevice.DisplayMode.Width,
                graphicsDevice.DisplayMode.Height);

            this._nextTarget = this._nextTarget ?? graphicsDevice.RenderFactory.CreateTarget(
                graphicsDevice.DisplayMode.Width,
                graphicsDevice.DisplayMode.Height);

            if (this.Transition.Current.IsVisible)
            {
                using (graphicsDevice.RenderTo(this._currentTarget))
                {
                    this.Transition.Current.Render();
                }
            }

            if (this.Transition.Next.IsVisible)
            {
                using (graphicsDevice.RenderTo(this._nextTarget))
                {
                    this.Transition.Next.Render();
                }
            }

            this.Transition.Render(this._currentTarget, this._nextTarget);
        }
        #endregion
    }
}
