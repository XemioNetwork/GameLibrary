using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Mono.Geometry
{
    public class MonoGeometryProvider : IGeometryProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoGeometryProvider"/> class.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        public MonoGeometryProvider(MonoRenderManager renderManager)
        {
            this._renderManager = renderManager;
            this.Factory = new MonoGeometryFactory();
        }
        #endregion

        #region Fields
        private MonoRenderManager _renderManager;
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
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawRectangle(
                MonoPen.GetNativePen(), MonoHelper.Convert(rectangle + Vector2.Truncate(this._renderManager.ScreenOffset)));
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
            MonoPen MonoPen = pen as MonoPen;

            start += Vector2.Truncate(this._renderManager.ScreenOffset);
            end += Vector2.Truncate(this._renderManager.ScreenOffset);

            this._renderManager.Graphics.DrawLine(
                MonoPen.GetNativePen(), start.X, start.Y, end.X, end.Y);
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
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawPolygon(
                MonoPen.GetNativePen(), MonoHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
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
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawEllipse(
                MonoPen.GetNativePen(), MonoHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)));
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
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawArc(
                MonoPen.GetNativePen(),
                MonoHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle, 
                sweepAngle);
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public void DrawPie(Color color, Rectangle region, float startAngle, float sweetAngle)
        {
            this.DrawPie(this.Factory.CreatePen(color), region, startAngle, sweetAngle);
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public void DrawPie(IPen pen, Rectangle region, float startAngle, float sweetAngle)
        {
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawPie(
                MonoPen.GetNativePen(),
                MonoHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle, 
                sweetAngle);
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
            MonoPen MonoPen = pen as MonoPen;

            this._renderManager.Graphics.DrawCurve(
                MonoPen.GetNativePen(), MonoHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            MonoBrush MonoBrush = brush as MonoBrush;

            this._renderManager.Graphics.FillRectangle(
                MonoBrush.GetNativeBrush(), MonoHelper.Convert(rectangle + Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        public void FillPolygon(IBrush brush, Vector2[] points)
        {
            MonoBrush MonoBrush = brush as MonoBrush;

            this._renderManager.Graphics.FillPolygon(
                MonoBrush.GetNativeBrush(), MonoHelper.Convert(points, Vector2.Truncate(this._renderManager.ScreenOffset)));
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public void FillEllipse(IBrush brush, Rectangle region)
        {
            MonoBrush MonoBrush = brush as MonoBrush;

            this._renderManager.Graphics.FillEllipse(
                MonoBrush.GetNativeBrush(), MonoHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)));
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
        /// <param name="sweetAngle">The sweet angle.</param>
        public void FillPie(IBrush brush, Rectangle region, float startAngle, float sweetAngle)
        {
            MonoBrush MonoBrush = brush as MonoBrush;

            this._renderManager.Graphics.FillPie(
                MonoBrush.GetNativeBrush(),
                MonoHelper.Convert(region + Vector2.Truncate(this._renderManager.ScreenOffset)),
                startAngle,
                sweetAngle);
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        public IGeometryFactory Factory { get; private set; }
        #endregion
    }
}
