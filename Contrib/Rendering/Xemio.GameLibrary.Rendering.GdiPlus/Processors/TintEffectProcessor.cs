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
            this._colors = new Stack<Color>();
        }
        #endregion

        #region Fields
        private Color _current;
        private readonly Stack<Color> _colors; 
        #endregion

        #region Methods
        /// <summary>
        /// Sets the opacity.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="color">The color.</param>
        private void Tint(GdiRenderManager renderManager, Color color)
        {
            this._current = color;

            if (color == Color.White)
            {
                renderManager.Attributes = null;
                return;
            }

            float a = color.A / 255.0f;
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            var matrix = new ColorMatrix(new[]
            {
                new[] { r, 0, 0, 0, 0 },
                new[] { 0, g, 0, 0, 0 },
                new[] { 0, 0, b, 0, 0 },
                new[] { 0, 0, 0, a, 0 },
                new[] { 0, 0, 0, 0, 1f }
            });

            renderManager.Attributes = new ImageAttributes();
            renderManager.Attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
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
            var gdiRenderManager = (GdiRenderManager)renderManager;

            this._colors.Push(this._current);
            this.Tint(gdiRenderManager, effect.Color);
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="renderManager">The render manager.</param>
        protected override void Disable(TintEffect effect, IRenderManager renderManager)
        {
            this.Tint((GdiRenderManager)renderManager, this._colors.Pop());
        }
        #endregion
    }
}
