using System;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.SharpDX.Geometry;

using GDIBitmap = System.Drawing.Bitmap;
using GDIImage = System.Drawing.Image;
using GDIRectangle = System.Drawing.Rectangle;

using Factory = SharpDX.Direct2D1.Factory;
using DXColor = SharpDX.Color;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    public static class SharpDXHelper
    {
        #region Properties
        /// <summary>
        /// Gets the SharpDX render target.
        /// </summary>
        public static RenderTarget RenderTarget { get; set; }
        /// <summary>
        /// Gets the SharpDX Direct2D factory.
        /// </summary>
        public static Factory Factory2D { get; internal set; }
        /// <summary>
        /// Gets the SharpDX swap chain.
        /// </summary>
        public static SwapChain SwapChain { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Converts a rectangle to a sharpdx rectangle.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static RectangleF ConvertRectangle(Math.Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
        }
        /// <summary>
        /// Creates a new SharpDX.DrawingPointF.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static DrawingPointF CreateDrawingPoint(Math.Vector2 point)
        {
            return new DrawingPointF(point.X, point.Y);
        }
        /// <summary>
        /// Creates a new SharpDX.Color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DXColor CreateColor(Color color)
        {
            return new DXColor(color.R, color.G, color.B, color.A);
        }
        /// <summary>
        /// Creates a brush from a pen.
        /// </summary>
        /// <param name="pen">The pen</param>
        /// <returns></returns>
        public static Brush GetBrush(IPen pen)
        {
            SharpDXPen sdxPen = pen as SharpDXPen;
            if (sdxPen == null)
            {
                throw new ArgumentException("Argument has to be SharpDXPen", "pen");
            }

            return sdxPen.Brush;
        }
        #endregion
    }
}
