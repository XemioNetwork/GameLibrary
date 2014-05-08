using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;

namespace Xemio.GameLibrary.Rendering
{
    public abstract class AbstractRenderManager : IRenderManager
    {
        #region Implementation of IRenderManager
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public abstract void Clear(Color color);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public abstract void Render(ITexture texture, Rectangle destination, Rectangle origin);
        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public virtual void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            this.DrawPolygon(pen, rectangle.Corners);
        }
        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public virtual void DrawRectangle(IPen pen, Rectangle rectangle, float cornerRadius)
        {
            if (cornerRadius > 0)
            {
                this.DrawArc(pen, new Rectangle(rectangle.Left, rectangle.Top, cornerRadius, cornerRadius),
                    MathHelper.ToRadians(-90), MathHelper.ToRadians(90));

                this.DrawLine(pen, new Vector2(rectangle.Left + cornerRadius, rectangle.Top),
                    new Vector2(rectangle.Right - cornerRadius, rectangle.Top));

                this.DrawArc(pen,
                    new Rectangle(rectangle.Right - cornerRadius, rectangle.Top, cornerRadius, cornerRadius),
                    MathHelper.ToRadians(0), MathHelper.ToRadians(90));

                this.DrawLine(pen, new Vector2(rectangle.Right, rectangle.Top + cornerRadius),
                    new Vector2(rectangle.Right, rectangle.Bottom - cornerRadius));

                this.DrawArc(pen,
                    new Rectangle(rectangle.Right - cornerRadius, rectangle.Bottom - cornerRadius, cornerRadius,
                        cornerRadius), MathHelper.ToRadians(90), MathHelper.ToRadians(90));

                this.DrawLine(pen, new Vector2(rectangle.Right - cornerRadius, rectangle.Bottom),
                    new Vector2(rectangle.Left + cornerRadius, rectangle.Bottom));

                this.DrawArc(pen,
                    new Rectangle(rectangle.Left, rectangle.Bottom - cornerRadius, cornerRadius, cornerRadius),
                    MathHelper.ToRadians(180), MathHelper.ToRadians(90));

                this.DrawLine(pen, new Vector2(rectangle.Left, rectangle.Bottom - cornerRadius),
                    new Vector2(rectangle.Left, rectangle.Top));
            }
            else
            {
                this.DrawRectangle(pen, rectangle);
            }
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public virtual void DrawEllipse(IPen pen, Rectangle region)
        {
            this.DrawArc(pen, region, 0, MathHelper.ToRadians(360));
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        public virtual void DrawPolygon(IPen pen, Vector2[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 start = vertices[i];
                Vector2 end = vertices[i + 1 >= vertices.Length ? 0 : i + 1];

                this.DrawLine(pen, start, end);
            }
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public abstract void DrawLine(IPen pen, Vector2 start, Vector2 end);
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public abstract void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public virtual void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            this.DrawArc(pen, region, startAngle, sweepAngle);

            float radiusX = region.Width * 0.5f;
            float radiusY = region.Height * 0.5f;

            float startAngleX = MathHelper.Sin(startAngle) * radiusX + region.Center.X;
            float startAngleY = MathHelper.Cos(startAngle) * radiusY + region.Center.Y;

            float endAngleX = MathHelper.Sin(startAngle + sweepAngle) * radiusX + region.Center.X;
            float endAngleY = MathHelper.Cos(startAngle + sweepAngle) * radiusY + region.Center.Y;

            this.DrawLine(pen, region.Center, new Vector2(startAngleX, startAngleY));
            this.DrawLine(pen, region.Center, new Vector2(endAngleX, endAngleY));
        }
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public abstract void FillRectangle(IBrush brush, Rectangle rectangle);
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public abstract void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius);
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public virtual void FillEllipse(IBrush brush, Rectangle region)
        {
            this.FillPie(brush, region, 0, MathHelper.ToRadians(360));
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        public abstract void FillPolygon(IBrush brush, Vector2[] vertices);
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public abstract void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public abstract void Present();
        #endregion
    }
}
