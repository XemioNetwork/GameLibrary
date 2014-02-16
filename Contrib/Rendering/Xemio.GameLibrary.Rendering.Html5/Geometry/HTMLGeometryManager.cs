using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.HTML5.Geometry
{
    public class HTMLGeometryManager : IGeometryManager
    {
        #region Constructors
        public HTMLGeometryManager()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public HTMLRenderManager RenderManager
        {
            get { return XGL.Components.Require<GraphicsDevice>().RenderManager as HTMLRenderManager; }
        }
        #endregion

        #region Methods

        #endregion

        #region Implementation of IGeometryManager

        public void DrawRectangle(Color color, Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawRoundedRectangle(Color color, Rectangle rectangle, float radius)
        {
            throw new NotImplementedException();
        }

        public void DrawRoundedRectangle(IPen pen, Rectangle rectangle, float radius)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Color color, Vector2 start, Vector2 end)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Color color, Vector2[] points)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(IPen pen, Vector2[] points)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(Color color, Rectangle region)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(IPen pen, Rectangle region)
        {
            throw new NotImplementedException();
        }

        public void DrawCircle(Color color, Vector2 position, float radius)
        {
            throw new NotImplementedException();
        }

        public void DrawCircle(IPen pen, Vector2 position, float radius)
        {
            throw new NotImplementedException();
        }

        public void DrawArc(Color color, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawPie(Color color, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawCurve(Color color, Vector2[] points)
        {
            throw new NotImplementedException();
        }

        public void DrawCurve(IPen pen, Vector2[] points)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            this.RenderManager.Context.fillStyle = HTMLHelper.Convert(Color.Red);
            this.RenderManager.Context.fillRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void FillRoundedRectangle(IBrush brush, Rectangle rectangle, float radius)
        {
            throw new NotImplementedException();
        }

        public void FillPolygon(IBrush brush, Vector2[] points)
        {
            throw new NotImplementedException();
        }

        public void FillEllipse(IBrush brush, Rectangle region)
        {
            throw new NotImplementedException();
        }

        public void FillCircle(IBrush brush, Vector2 position, float radius)
        {
            throw new NotImplementedException();
        }

        public void FillArc(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
