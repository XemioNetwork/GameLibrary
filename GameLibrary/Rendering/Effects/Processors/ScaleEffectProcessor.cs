using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Events;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    public class ScaleEffectProcessor : EffectProcessor<ScaleEffect>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleEffectProcessor"/> class.
        /// </summary>
        public ScaleEffectProcessor()
        {
            this.Scale = new Vector2(1, 1);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public Vector2 Scale { get; set; }
        #endregion

        #region Overrides of EffectProcessor<ScaleEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Enable(ScaleEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Scale *= effect.Scale;
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Disable(ScaleEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Scale /= effect.Scale;
        }
        /// <summary>
        /// Called when a region gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public override void OnRegion(GraphicsDevice graphicsDevice, RegionEvent evt)
        {
            evt.Region *= this.Scale;
        }
        /// <summary>
        /// Called when vertices are getting rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public override void OnVertices(GraphicsDevice graphicsDevice, VertexEvent evt)
        {
            for (int i = 0; i < evt.Vertices.Length; i++)
            {
                evt.Vertices[i] *= this.Scale;
            }
        }
        /// <summary>
        /// Called when a line gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public override void OnDrawLine(GraphicsDevice graphicsDevice, DrawLineEvent evt)
        {
            evt.Start *= this.Scale;
            evt.End *= this.Scale;
        }
        #endregion
    }
}
