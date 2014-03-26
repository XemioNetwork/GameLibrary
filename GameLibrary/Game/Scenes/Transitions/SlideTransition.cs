using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes.Transitions
{
    public class SlideTransition : ITransition
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SlideTransition" /> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="duration">The duration.</param>
        public SlideTransition(Vector2 direction, float duration)
        {
            var graphicsDevice = XGL.Components.Require<GraphicsDevice>();

            var max = MathHelper.Max(MathHelper.Abs(direction.X), MathHelper.Abs(direction.Y));
            var normalized = direction / max;
            
            this.Direction = normalized * graphicsDevice.DisplayMode;
            this.Duration = duration;
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Vector2 Direction { get; private set; }
        /// <summary>
        /// Gets the duration.
        /// </summary>
        public float Duration { get; private set; }
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

            float percentage = this._elapsed / this.Duration;
            Vector2 translation = Vector2.SmoothStep(Vector2.Zero, this.Direction, percentage);

            using (graphicsDevice.RenderManager.Translate(-translation))
            {
                graphicsDevice.RenderManager.Render(current, Vector2.Zero);
                graphicsDevice.RenderManager.Render(next, this.Direction);
            }
        }
        #endregion
    }
}
