using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;

namespace Xemio.GameLibrary.Rendering.GdiPlus.Processors
{
    public class TintEffectProcessor : EffectProcessor<TintEffect>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TintEffectProcessor"/> class.
        /// </summary>
        public TintEffectProcessor()
        {
            this.Effects = new List<TintEffect>();
        }
        #endregion

        #region Properties
        public List<TintEffect> Effects { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the opacity.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        private void Apply(GdiRenderManager renderManager)
        {
            if (this.Effects.Count == 0)
            {
                renderManager.Attributes = null;
                return;
            }

            var color = new Color();
            color = this.Effects.Aggregate(color, (current, effect) => effect.BlendMode.Combine(current, effect.Color));

            float m = 1.0f / 255.0f;
            float a = color.A * m;
            float r = color.R * m;
            float g = color.G * m;
            float b = color.B * m;

            var matrix = new ColorMatrix(new[]
            {
                new[] { r, 0, 0, 0, 0 },
                new[] { 0, g, 0, 0, 0 },
                new[] { 0, 0, b, 0, 0 },
                new[] { 0, 0, 0, a, 0 },
                new[] { 0, 0, 0, 0, 1f }
            });

            renderManager.Attributes = new ImageAttributes();
            renderManager.Attributes.SetColorMatrix(matrix, 
                ColorMatrixFlag.Default, 
                ColorAdjustType.Bitmap);
        }
        #endregion

        #region Overrides of EffectProcessor<TintEffect>
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Enable(TintEffect effect, IRenderManager renderManager)
        {
            this.Effects.Add(effect);
            this.Apply((GdiRenderManager)renderManager);
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Disable(TintEffect effect, IRenderManager renderManager)
        {
            this.Effects.RemoveAt(this.Effects.Count - 1);
            this.Apply((GdiRenderManager)renderManager);
        }
        #endregion
    }
}
