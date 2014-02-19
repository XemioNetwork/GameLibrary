using System;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Effects;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullRenderManager : IRenderManager
    {
        #region Implementation of IRenderManager
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
        }
        /// <summary>
        /// Applies the specified effects.
        /// </summary>
        /// <param name="effects">The effects.</param>
        public IDisposable Apply(params IEffect[] effects)
        {
            return ActionDisposable.Empty;
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
        }
        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
        }
        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle, float cornerRadius)
        {
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(IPen pen, Rectangle region)
        {
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        public void DrawPolygon(IPen pen, Vector2[] vertices)
        {
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
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
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
        }
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public void FillEllipse(IBrush brush, Rectangle region)
        {
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        public void FillPolygon(IBrush brush, Vector2[] vertices)
        {
        }
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
        }
        /// <summary>
        /// Presents this instance.
        /// </summary>
        public void Present()
        {
        }
        #endregion
    }
}
