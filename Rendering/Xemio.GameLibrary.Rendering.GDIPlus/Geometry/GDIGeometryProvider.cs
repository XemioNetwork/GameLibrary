using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Drawing2D;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.GDIPlus.Geometry
{
    public class GDIGeometryProvider : IGeometryProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GDIGeometryProvider"/> class.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        public GDIGeometryProvider(GDIRenderManager renderManager)
        {
            this._renderManager = renderManager;
            this.Factory = new GDIGeometryFactory();
        }
        #endregion

        #region Fields
        private GDIRenderManager _renderManager;
        #endregion

        #region IGeometryProvider Member
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(Color color, Rectangle rectangle)
        {
            this.DrawRectangle(this.Factory.CreatePen(color), rectangle);
        }
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawRectangle(
                gdiPen.GetNativePen(), GDIHelper.Convert(rectangle + Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void DrawRoundedRectangle(Color color, Rectangle rectangle, float radius)
        {
            this.DrawRoundedRectangle(this.Factory.CreatePen(color), rectangle, radius);
        }
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void DrawRoundedRectangle(IPen pen, Rectangle rectangle, float radius)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawPath(
                gdiPen.GetNativePen(), this.PrepareRoundedRectanglePath(rectangle, radius));
        }
        /// <summary>
        /// Prepares a GraphicsPath for the rounded rectangle
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The corner radius.</param>
        /// <returns></returns>
        private GraphicsPath PrepareRoundedRectanglePath(Rectangle rectangle, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0.0f)
            {
                path.AddRectangle(new System.Drawing.RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
                path.CloseFigure();

                return path;
            }

            path.AddArc(rectangle.X - 1, rectangle.Y - 1, radius * 2, radius * 2, -180, 90);
            path.AddArc(rectangle.X + rectangle.Width - radius * 2, rectangle.Y - 1, radius * 2, radius * 2, -90, 90);
            path.AddArc(rectangle.X + rectangle.Width - radius * 2, rectangle.Y + rectangle.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rectangle.X - 1, rectangle.Y + rectangle.Height - radius * 2, radius * 2, radius * 2, 90, 90);

            path.CloseFigure();

            return path;
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(Color color, Vector2 start, Vector2 end)
        {
            this.DrawLine(this.Factory.CreatePen(color), start, end);
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
            GDIPen gdiPen = pen as GDIPen;

            start += Vector2.Truncate(this._renderManager.ScreenOffset);
            end += Vector2.Truncate(this._renderManager.ScreenOffset);

            this._renderManager.Graphics.DrawLine(
                gdiPen.GetNativePen(), start.X, start.Y, end.X, end.Y);
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(Color color, Vector2[] points)
        {
            this.DrawPolygon(this.Factory.CreatePen(color), points);
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(IPen pen, Vector2[] points)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawPolygon(
                gdiPen.GetNativePen(), GDIHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(Color color, Rectangle region)
        {
            this.DrawEllipse(this.Factory.CreatePen(color), region);
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(IPen pen, Rectangle region)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawEllipse(
                gdiPen.GetNativePen(), GDIHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void DrawCircle(Color color, Vector2 position, float radius)
        {
            this.DrawEllipse(color, new Rectangle(position.X - radius, position.Y - radius, radius * 2, radius * 2));
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void DrawCircle(IPen pen, Vector2 position, float radius)
        {
            this.DrawEllipse(pen, new Rectangle(position.X - radius, position.Y - radius, radius * 2, radius * 2));
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawArc(Color color, Rectangle region, float startAngle, float sweepAngle)
        {
            this.DrawArc(this.Factory.CreatePen(color), region, startAngle, sweepAngle);
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawArc(
                gdiPen.GetNativePen(),
                GDIHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle, 
                sweepAngle);
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        public void DrawPie(Color color, Rectangle region, float startAngle, float sweepAngle)
        {
            this.DrawPie(this.Factory.CreatePen(color), region, startAngle, sweepAngle);
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        public void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawPie(
                gdiPen.GetNativePen(),
                GDIHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle, 
                sweepAngle);
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(Color color, Vector2[] points)
        {
            this.DrawCurve(this.Factory.CreatePen(color), points);
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(IPen pen, Vector2[] points)
        {
            GDIPen gdiPen = pen as GDIPen;

            this._renderManager.Graphics.DrawCurve(
                gdiPen.GetNativePen(), GDIHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            GDIBrush gdiBrush = brush as GDIBrush;

            this._renderManager.Graphics.FillRectangle(
                gdiBrush.GetNativeBrush(), GDIHelper.Convert(rectangle + Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRoundedRectangle(IBrush brush, Rectangle rectangle, float radius)
        {
            GDIBrush gdiBrush = brush as GDIBrush;

            this._renderManager.Graphics.FillPath(
                gdiBrush.GetNativeBrush(), this.PrepareRoundedRectanglePath(rectangle, radius));
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        public void FillPolygon(IBrush brush, Vector2[] points)
        {
            GDIBrush gdiBrush = brush as GDIBrush;

            this._renderManager.Graphics.FillPolygon(
                gdiBrush.GetNativeBrush(), GDIHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public void FillEllipse(IBrush brush, Rectangle region)
        {
            GDIBrush gdiBrush = brush as GDIBrush;

            this._renderManager.Graphics.FillEllipse(
                gdiBrush.GetNativeBrush(), GDIHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills a circle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void FillCircle(IBrush brush, Vector2 position, float radius)
        {
            this.FillEllipse(brush, new Rectangle(position.X - radius, position.Y - radius, radius * 2, radius * 2));
        }
        /// <summary>
        /// Fills an arc.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void FillArc(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            this.FillPie(brush, region, startAngle, sweepAngle);
        }
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        public void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            GDIBrush gdiBrush = brush as GDIBrush;

            this._renderManager.Graphics.FillPie(
                gdiBrush.GetNativeBrush(),
                GDIHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle,
                sweepAngle);
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        public IGeometryFactory Factory { get; private set; }
        #endregion
    }
}
