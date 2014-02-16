using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.GdiPlus.Geometry;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class GdiRenderFactory : IRenderFactory
    {
        #region Implementation of IRenderFactory
        /// <summary>
        /// Creates a render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IRenderTarget CreateTarget(int width, int height)
        {
            return new GdiRenderTarget(width, height);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        public IPen CreatePen(Color color)
        {
            return new GdiPen(color, 1.0f);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public IPen CreatePen(Color color, float thickness)
        {
            return new GdiPen(color, thickness);
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        public IBrush CreateSolidBrush(Color color)
        {
            return new GdiBrush(new SolidBrush(GdiHelper.Convert(color)));
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
            var rectangle = new Rectangle(from.X, from.Y, to.X - from.X, to.Y - from.Y);
            var angle = MathHelper.ToDegrees(MathHelper.ToAngle(to - from));

            return new GdiBrush(
                new LinearGradientBrush(GdiHelper.Convert(rectangle), GdiHelper.Convert(top), GdiHelper.Convert(bottom), angle));
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public IBrush CreateTextureBrush(ITexture texture)
        {
            var gdiTexture = texture as GdiTexture;
            if (gdiTexture == null)
            {
                throw new ArgumentException("Invalid texture.", "texture");
            }

            return new GdiBrush(new TextureBrush(gdiTexture.Bitmap));
        }
        #endregion
    }
}
