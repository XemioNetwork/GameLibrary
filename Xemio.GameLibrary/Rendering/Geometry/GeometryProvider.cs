using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public class GeometryProvider : IGeometryProvider
    {
        #region Singleton
        /// <summary>
        /// Gets the empty geometry provider.
        /// </summary>
        public static IGeometryProvider Empty
        {
            get { return Singleton<GeometryProvider>.Value; }
        }
        #endregion
        
        #region IGeometryProvider Member
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        public virtual void DrawRectangle(Color color, Rectangle rectangle)
        {
        }
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public virtual void DrawRectangle(IPen pen, Rectangle rectangle)
        {
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public virtual void DrawLine(Color color, Vector2 start, Vector2 end)
        {
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public virtual void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public virtual void DrawPolygon(Color color, Vector2[] points)
        {
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public virtual void DrawPolygon(IPen pen, Vector2[] points)
        {
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        public virtual void DrawEllipse(Color color, Rectangle region)
        {
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public virtual void DrawEllipse(IPen pen, Rectangle region)
        {
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public virtual void DrawCircle(Color color, Vector2 position, float radius)
        {
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public virtual void DrawCircle(IPen pen, Vector2 position, float radius)
        {
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public virtual void DrawArc(Color color, Rectangle region, float startAngle, float sweepAngle)
        {
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public virtual void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public virtual void DrawPie(Color color, Rectangle region, float startAngle, float sweetAngle)
        {
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public virtual void DrawPie(IPen pen, Rectangle region, float startAngle, float sweetAngle)
        {
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public virtual void DrawCurve(Color color, Vector2[] points)
        {
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public virtual void DrawCurve(IPen pen, Vector2[] points)
        {
        }
        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public virtual void FillRectangle(IBrush brush, Rectangle rectangle)
        {
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        public virtual void FillPolygon(IBrush brush, Vector2[] points)
        {
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public virtual void FillEllipse(IBrush brush, Rectangle region)
        {
        }
        /// <summary>
        /// Fills a circle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public virtual void FillCircle(IBrush brush, Vector2 position, float radius)
        {
        }
        /// <summary>
        /// Fills an arc.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public virtual void FillArc(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
        }
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public virtual void FillPie(IBrush brush, Rectangle region, float startAngle, float sweetAngle)
        {
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        public virtual IGeometryFactory Factory
        {
            get { return GeometryFactory.Empty; }
        }
        #endregion
    }
}
