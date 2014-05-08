using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Events;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    public class TranslateEffectProcessor : EffectProcessor<TranslateEffect>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        #endregion

        #region Overrides of EffectProcessor<TranslateEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Enable(TranslateEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Offset += effect.Offset;
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected override void Disable(TranslateEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Offset -= effect.Offset;
        }
        /// <summary>
        /// Called when a region gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public override void OnRegion(GraphicsDevice graphicsDevice, RegionEvent evt)
        {
            evt.Region += this.Offset;
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
                evt.Vertices[i] += this.Offset;
            }
        }
        /// <summary>
        /// Called when a line gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public override void OnDrawLine(GraphicsDevice graphicsDevice, DrawLineEvent evt)
        {
            evt.Start += this.Offset;
            evt.End += this.Offset;
        }
        #endregion
    }
}
