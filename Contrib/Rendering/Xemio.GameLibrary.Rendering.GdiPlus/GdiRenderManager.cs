using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.GdiPlus.Geometry;
using Xemio.GameLibrary.Rendering.Shapes;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    using Drawing = System.Drawing;
    using Drawing2D = System.Drawing.Drawing2D;

    using Rectangle = Xemio.GameLibrary.Math.Rectangle;
    using Color = System.Drawing.Color;

    public abstract class GdiRenderManager : BaseRenderManager
    {
        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Require<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the smoothing mode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }
        /// <summary>
        /// Gets the buffer graphics.
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                var renderTarget = (GdiRenderTarget)this.GraphicsDevice.RenderTarget;

                Graphics graphics = renderTarget.Graphics;
                graphics.SmoothingMode = Gdi.Convert(this.SmoothingMode);
                graphics.InterpolationMode = Gdi.Convert(this.InterpolationMode);

                return graphics;
            }
        }
        #endregion

        #region Effect Properties
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public ImageAttributes Attributes { get; set; }
        #endregion
        
        #region IRenderProvider Member
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public override void Clear(Rendering.Color color)
        {
            this.Graphics.Clear(Gdi.Convert(color));
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public override void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
            this.Graphics.DrawLine(((GdiPen)pen).Pen, start.X + this.Offset.X, start.Y + this.Offset.Y, end.X + this.Offset.X, end.Y + this.Offset.Y);
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public override void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            this.Graphics.DrawArc(((GdiPen)pen).Pen, region.X + this.Offset.X, region.Y + this.Offset.Y, region.Width, region.Height, startAngle, sweepAngle);
        }
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public override void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            this.Graphics.FillRectangle(((GdiBrush)brush).Brush, rectangle.X + this.Offset.X, rectangle.Y + this.Offset.Y, rectangle.Width, rectangle.Height);
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public override void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public override void FillEllipse(IBrush brush, Rectangle region)
        {
            this.Graphics.FillEllipse(((GdiBrush)brush).Brush, region.X + this.Offset.X, region.Y + this.Offset.Y, region.Width, region.Height);
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        public override void FillPolygon(IBrush brush, Vector2[] vertices)
        {
            this.Graphics.FillPolygon(((GdiBrush)brush).Brush, Gdi.Convert(vertices, this.Offset));
        }
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public override void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            this.Graphics.FillPie(((GdiBrush)brush).Brush, region.X + this.Offset.X, region.Y + this.Offset.Y, region.Width, region.Height, startAngle, sweepAngle);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public override void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            var gdiTexture = texture as GdiTexture;
            var surface = XGL.Components.Require<WindowSurface>();

            Control control = surface.Control;
            if (control == null)
            {
                return;
            }

            if (gdiTexture == null)
            {
                throw new InvalidOperationException("The rendered texture has to be an instance of the GdiTexture class.");
            }

            var x = (int)destination.X + (int)this.Offset.X;
            var y = (int)destination.Y + (int)this.Offset.Y;
            var w = (int)destination.Width;
            var h = (int)destination.Height;

            if (this.Attributes != null)
            {
                this.Graphics.DrawImage(
                    gdiTexture.Bitmap,
                    new Drawing.Rectangle(x, y, w, h),
                    origin.X, origin.Y, origin.Width, origin.Height, GraphicsUnit.Pixel,
                    this.Attributes);
            }
            else
            {
                this.Graphics.DrawImage(
                    gdiTexture.Bitmap, 
                    new Drawing.Rectangle(x, y, w, h),
                    Gdi.Convert(origin),
                    GraphicsUnit.Pixel);
            }

            this.Graphics.ResetTransform();
        }
        /// <summary>
        /// Presents this instance. 
        /// </summary>
        public override void Present()
        {
            this.Offset = Vector2.Zero;
            this.Graphics.Clear(Color.Black);
        }
        #endregion
    }
}
