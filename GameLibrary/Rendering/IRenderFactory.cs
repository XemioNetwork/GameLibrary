using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.GameLibrary.Rendering
{
    public interface IRenderFactory
    {
        /// <summary>
        /// Creates a new font for the specified parameters.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        IFont CreateFont(string fontFamily, float size);
        /// <summary>
        /// Creates a new render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        IRenderTarget CreateTarget(int width, int height);
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        IPen CreatePen(Color color);
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        IPen CreatePen(Color color, float thickness);
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        IBrush CreateSolidBrush(Color color);
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        IBrush CreateGradientBrush(Color top, Color bottom, Vector2 from, Vector2 to);
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        IBrush CreateTextureBrush(ITexture texture);
    }
}
