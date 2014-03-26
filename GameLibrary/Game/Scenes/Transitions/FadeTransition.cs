using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes.Transitions
{
    public class FadeTransition : ITransition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FadeTransition"/> class.
        /// </summary>
        public FadeTransition() : this(300)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FadeTransition" /> class.
        /// </summary>
        /// <param name="duration">The duration in milliseconds.</param>
        public FadeTransition(float duration)
        {
            this.Duration = duration;
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the duration in milliseconds.
        /// </summary>
        public float Duration { get; set; }
        #endregion

        #region Implementation of ITransition
        /// <summary>
        /// Gets a value indicating whether the transition is completed.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= this.Duration)
            {
                this.IsCompleted = true;
            }
        }
        /// <summary>
        /// Enters the transition.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="next">The next.</param>
        public void Enter(Scene current, Scene next)
        {
        }
        /// <summary>
        /// Leaves the transition. NOTE: Current contains now the scene, that was declared as next before.
        /// </summary>
        /// <param name="current">The current.</param>
        public void Leave(Scene current)
        {
        }
        /// <summary>
        /// Renders a transition between the specified scenes.
        /// </summary>
        /// <param name="current">The current scenes frame.</param>
        /// <param name="next">The next scenes frame.</param>
        public void Render(IRenderTarget current, IRenderTarget next)
        {
            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();

            using (graphicsDevice.RenderManager.Alpha(1.0f - this._elapsed / this.Duration))
            {
                graphicsDevice.RenderManager.Render(current, Vector2.Zero);
            }
            using (graphicsDevice.RenderManager.Alpha(this._elapsed / this.Duration))
            {
                graphicsDevice.RenderManager.Render(next, Vector2.Zero);
            }
        }
        #endregion
    }
}
