using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public interface IGeometryManager
    {
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        void DrawRectangle(Color color, Rectangle rectangle);
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle);
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        void DrawRoundedRectangle(Color color, Rectangle rectangle, float radius);
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        void DrawRoundedRectangle(IPen pen, Rectangle rectangle, float radius);
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        void DrawLine(Color color, Vector2 start, Vector2 end);
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        void DrawLine(IPen pen, Vector2 start, Vector2 end);
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        void DrawPolygon(Color color, Vector2[] points);
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        void DrawPolygon(IPen pen, Vector2[] points);
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        void DrawEllipse(Color color, Rectangle region);
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        void DrawEllipse(IPen pen, Rectangle region);
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        void DrawCircle(Color color, Vector2 position, float radius);
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        void DrawCircle(IPen pen, Vector2 position, float radius);
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void DrawArc(Color color, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        void DrawPie(Color color, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        void DrawCurve(Color color, Vector2[] points);
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        void DrawCurve(IPen pen, Vector2[] points);
        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        void FillRectangle(IBrush brush, Rectangle rectangle);
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        void FillRoundedRectangle(IBrush brush, Rectangle rectangle, float radius);
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        void FillPolygon(IBrush brush, Vector2[] points);
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        void FillEllipse(IBrush brush, Rectangle region);
        /// <summary>
        /// Fills a circle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        void FillCircle(IBrush brush, Vector2 position, float radius);
        /// <summary>
        /// Fills an arc.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void FillArc(IBrush brush, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle);
    }
}
