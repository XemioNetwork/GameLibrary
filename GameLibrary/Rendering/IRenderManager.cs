using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.GameLibrary.Rendering
{
    public interface IRenderManager
    {
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        void Clear(Color color);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        void Render(ITexture texture, Rectangle destination, Rectangle origin);
        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle);
        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle, float cornerRadius);
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        void DrawEllipse(IPen pen, Rectangle region);
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        void DrawPolygon(IPen pen, Vector2[] vertices);
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        void DrawLine(IPen pen, Vector2 start, Vector2 end);
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
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        void FillRectangle(IBrush brush, Rectangle rectangle);
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius);
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        void FillEllipse(IBrush brush, Rectangle region);
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        void FillPolygon(IBrush brush, Vector2[] vertices);
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Presents this instance.
        /// </summary>
        void Present();
    }
}
