using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullRenderFactory : IRenderFactory
    {
        #region Implementation of IRenderFactory
        /// <summary>
        /// Creates a new font for the specified parameters.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        public IFont CreateFont(string fontFamily, float size)
        {
            return new NullFont(fontFamily, size);
        }
        /// <summary>
        /// Creates a new render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IRenderTarget CreateTarget(int width, int height)
        {
            return new NullTexture(width, height);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        public IPen CreatePen(Color color)
        {
            return new NullPen(color, 1);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public IPen CreatePen(Color color, float thickness)
        {
            return new NullPen(color, thickness);
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        public IBrush CreateSolidBrush(Color color)
        {
            return new NullBrush(0, 0);
        }
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        public IBrush CreateGradientBrush(Color top, Color bottom, Vector2 @from, Vector2 to)
        {
            return new NullBrush((int)(to.X - from.X), (int)(to.Y - from.Y));
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public IBrush CreateTextureBrush(ITexture texture)
        {
            return new NullBrush(texture.Width, texture.Height);
        }
        #endregion
    }
}
