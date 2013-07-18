using System.Linq;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI.Widgets.Base
{
    public class WidgetGraphics : IGraphics
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetGraphics"/> class.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public WidgetGraphics(Widget widget)
        {
            this.Widget = widget;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the widget.
        /// </summary>
        public Widget Widget { get; private set; }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return this.GraphicsDevice.RenderManager; }
        }
        /// <summary>
        /// Gets the geometry.
        /// </summary>
        public IGeometryProvider Geometry
        {
            get { return this.GraphicsDevice.Geometry; }
        }
        #endregion

        #region Implementation of IRenderManager
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return XGL.Components.Get<GraphicsDevice>(); }
        }
        /// <summary>
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer
        {
            get { return this.RenderManager.BackBuffer; }
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.RenderManager.Clear(color);
        }
        /// <summary>
        /// Offsets the screen.
        /// </summary>
        /// <param name="translation">The translation.</param>
        public void Translate(Vector2 translation)
        {
            this.RenderManager.Translate(translation);
        }
        /// <summary>
        /// Sets the rotation to the specified angle in radians.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void Rotate(float rotation)
        {
            this.RenderManager.Rotate(rotation);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Vector2 position)
        {
            this.RenderManager.Render(texture, position + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Vector2 position, Color color)
        {
            this.RenderManager.Render(texture, position + this.Widget.AbsolutePosition, color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Rectangle destination)
        {
            this.RenderManager.Render(texture, destination + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Color color)
        {
            this.RenderManager.Render(texture, destination + this.Widget.AbsolutePosition, color);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            this.RenderManager.Render(texture, destination + this.Widget.AbsolutePosition, origin);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="color">The color.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin, Color color)
        {
            this.RenderManager.Render(texture, destination + this.Widget.AbsolutePosition, origin, color);
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
            this.RenderManager.Present();
        }
        #endregion

        #region Implementation of IGeometryProvider
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(Color color, Rectangle rectangle)
        {
            this.Geometry.DrawRectangle(color, rectangle + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            this.Geometry.DrawRectangle(pen, rectangle + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void DrawRoundedRectangle(Color color, Rectangle rectangle, float radius)
        {
            this.Geometry.DrawRoundedRectangle(color, rectangle, radius);
        }
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void DrawRoundedRectangle(IPen pen, Rectangle rectangle, float radius)
        {
            this.Geometry.DrawRoundedRectangle(pen, rectangle, radius);
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(Color color, Vector2 start, Vector2 end)
        {
            this.Geometry.DrawLine(color, start + this.Widget.AbsolutePosition, end + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
            this.Geometry.DrawLine(pen, start + this.Widget.AbsolutePosition, end + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(Color color, Vector2[] points)
        {
            this.Geometry.DrawPolygon(color, points.Select(p => p + this.Widget.AbsolutePosition).ToArray());
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawPolygon(IPen pen, Vector2[] points)
        {
            this.Geometry.DrawPolygon(pen, points.Select(p => p + this.Widget.AbsolutePosition).ToArray());
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(Color color, Rectangle region)
        {
            this.Geometry.DrawEllipse(color, region + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(IPen pen, Rectangle region)
        {
            this.Geometry.DrawEllipse(pen, region + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void DrawCircle(Color color, Vector2 position, float radius)
        {
            this.Geometry.DrawCircle(color, position + this.Widget.AbsolutePosition, radius);
        }
        /// <summary>
        /// Draws a circle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void DrawCircle(IPen pen, Vector2 position, float radius)
        {
            this.Geometry.DrawCircle(pen, position + this.Widget.AbsolutePosition, radius);
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
            this.Geometry.DrawArc(color, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
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
            this.Geometry.DrawArc(pen, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
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
            this.Geometry.DrawPie(color, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
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
            this.Geometry.DrawPie(pen, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(Color color, Vector2[] points)
        {
            this.Geometry.DrawCurve(color, points.Select(p => p + this.Widget.AbsolutePosition).ToArray());
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(IPen pen, Vector2[] points)
        {
            this.Geometry.DrawCurve(pen, points.Select(p => p + this.Widget.AbsolutePosition).ToArray());
        }
        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            this.Geometry.FillRectangle(brush, rectangle + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void FillRoundedRectangle(IBrush brush, Rectangle rectangle, float radius)
        {
            this.Geometry.FillRoundedRectangle(brush, rectangle, radius);
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="points">The points.</param>
        public void FillPolygon(IBrush brush, Vector2[] points)
        {
            this.Geometry.FillPolygon(brush, points.Select(p => p + this.Widget.AbsolutePosition).ToArray());
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public void FillEllipse(IBrush brush, Rectangle region)
        {
            this.Geometry.FillEllipse(brush, region + this.Widget.AbsolutePosition);
        }
        /// <summary>
        /// Fills a circle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="position">The position.</param>
        /// <param name="radius">The radius.</param>
        public void FillCircle(IBrush brush, Vector2 position, float radius)
        {
            this.Geometry.FillCircle(brush, position + this.Widget.AbsolutePosition, radius);
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
            this.Geometry.FillArc(brush, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
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
            this.Geometry.FillPie(brush, region + this.Widget.AbsolutePosition, startAngle, sweepAngle);
        }
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        public IGeometryFactory Factory
        {
            get { return this.GraphicsDevice.Geometry.Factory; }
        }
        #endregion

        #region Implementation of IGeometryFactory
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        public IPen CreatePen(Color color)
        {
            return this.Factory.CreatePen(color);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public IPen CreatePen(Color color, float thickness)
        {
            return this.Factory.CreatePen(color, thickness);
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        public IBrush CreateSolid(Color color)
        {
            return this.Factory.CreateSolid(color);
        }

        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        public IBrush CreateGradient(Color top, Color bottom, Vector2 from, Vector2 to)
        {
            return this.Factory.CreateGradient(top, bottom, from, to);
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public IBrush CreateTexture(ITexture texture)
        {
            return this.Factory.CreateTexture(texture);
        }
        #endregion
    }
}
