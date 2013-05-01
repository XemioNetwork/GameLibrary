using SharpDX.Direct2D1;
using System;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.SharpDX.Geometry
{
    class SharpDXGeometryProvider : IGeometryProvider
    {
        #region Constructor
        /// <summary>
        /// Initializes the SharpDXGeometryProvider
        /// </summary>
        public SharpDXGeometryProvider(SharpDXRenderManager renderManager)
        {
            this.Factory = new SharpDXGeometryFactory();
            this._renderManager = renderManager;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Geometry factory
        /// </summary>
        public IGeometryFactory Factory { get; private set; }
        #endregion

        #region Fields
        private SharpDXRenderManager _renderManager;
        #endregion

        #region Drawing Methods

        #region Arc
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawArc(IPen pen, Math.Rectangle region, float startAngle, float sweepAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawArc(Color color, Math.Rectangle region, float startAngle, float sweepAngle)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Circle
        /// <summary>
        /// Draws a circle
        /// </summary>
        /// <param name="pen">Pen of the circle</param>
        /// <param name="position">Center point of the circle</param>
        /// <param name="radius">Radius of the circle</param>
        public void DrawCircle(IPen pen, Math.Vector2 position, float radius)
        {
            Ellipse ellipse = new Ellipse(
                SharpDXHelper.CreateDrawingPoint(position + this._renderManager.ScreenOffset), 
                radius, 
                radius);
            SharpDXHelper.RenderTarget.DrawEllipse(ellipse, SharpDXHelper.GetBrush(pen), pen.Thickness);
        }

        /// <summary>
        /// Draws a circle
        /// </summary>
        /// <param name="color">Color of the pen</param>
        /// <param name="position">Center point of the circle</param>
        /// <param name="radius">Radius of the circle</param>
        public void DrawCircle(Color color, Math.Vector2 position, float radius)
        {
            this.DrawCircle(new SharpDXPen(color, 1.0f), position, radius);
        }
        #endregion

        #region Curve
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(IPen pen, Math.Vector2[] points)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Draws a curve.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="points">The points.</param>
        public void DrawCurve(Color color, Math.Vector2[] points)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Ellipse
        /// <summary>
        /// Draws an ellipse
        /// </summary>
        /// <param name="pen">Pen of the ellipse</param>
        /// <param name="region">Region of the ellipse</param>
        public void DrawEllipse(IPen pen, Math.Rectangle region)
        {
            Ellipse ellipse = new Ellipse(
                SharpDXHelper.CreateDrawingPoint(region.Center + this._renderManager.ScreenOffset), 
                region.Width / 2, 
                region.Height / 2);
            SharpDXHelper.RenderTarget.DrawEllipse(ellipse, SharpDXHelper.GetBrush(pen), pen.Thickness);
        }
        /// <summary>
        /// Draws an ellipse
        /// </summary>
        /// <param name="color">Color of the stroke</param>
        /// <param name="region">Region of the ellipse</param>
        public void DrawEllipse(Color color, Math.Rectangle region)
        {
            this.DrawEllipse(new SharpDXPen(color, 1.0f), region);
        }
        #endregion

        #region Line
        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="pen">Pen of the lin</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        public void DrawLine(IPen pen, Math.Vector2 start, Math.Vector2 end)
        {
            SharpDXHelper.RenderTarget.DrawLine(
                SharpDXHelper.CreateDrawingPoint(start + this._renderManager.ScreenOffset),
                SharpDXHelper.CreateDrawingPoint(end + this._renderManager.ScreenOffset), 
                SharpDXHelper.GetBrush(pen),
                pen.Thickness);
        }
        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="color">Color of the lin</param>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        public void DrawLine(Color color, Math.Vector2 start, Math.Vector2 end)
        {
            this.DrawLine(new SharpDXPen(color, 1.0f), start, end);
        }
        #endregion

        #region Pie
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public void DrawPie(IPen pen, Math.Rectangle region, float startAngle, float sweetAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweetAngle">The sweet angle.</param>
        public void DrawPie(Color color, Math.Rectangle region, float startAngle, float sweetAngle)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Polygon
        /// <summary>
        /// Draws a polygon
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        public void DrawPolygon(IPen pen, Math.Vector2[] points)
        {
            SharpDXPen sdxPen = pen as SharpDXPen;
            if(pen == null)
            {
                throw new ArgumentException("Argument has to be SharpDXPen", "pen");
            }

            if(points.Length != 0)
            {
                PathGeometry geometry = new PathGeometry(SharpDXHelper.Factory2D);
                using (GeometrySink sink = geometry.Open())
                {
                    sink.SetFillMode(FillMode.Winding);
                    sink.BeginFigure(SharpDXHelper.CreateDrawingPoint(points[points.Length - 1]), FigureBegin.Hollow);

                    for (int i = 0; i < points.Length; i++)
                    {
                        sink.AddLine(SharpDXHelper.CreateDrawingPoint(points[i]));
                    }

                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }

                SharpDXHelper.RenderTarget.DrawGeometry(geometry, sdxPen.Brush, sdxPen.Thickness);
            }
        }

        /// <summary>
        /// Draws a polygon
        /// </summary>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public void DrawPolygon(Color color, Math.Vector2[] points)
        {
            this.DrawPolygon(new SharpDXPen(color, 1.0f), points);
        }
        #endregion

        #region Rectangle
        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="pen">Pen</param>
        /// <param name="rectangle">Rectangle</param>
        public void DrawRectangle(IPen pen, Math.Rectangle rectangle)
        {
            SharpDXHelper.RenderTarget.DrawRectangle(
                SharpDXHelper.ConvertRectangle(rectangle + this._renderManager.ScreenOffset), 
                SharpDXHelper.GetBrush(pen), 
                pen.Thickness);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="color">Color of the stroke</param>
        /// <param name="rectangle">Rectangle</param>
        public void DrawRectangle(Color color, Math.Rectangle rectangle)
        {
            this.DrawRectangle(new SharpDXPen(color, 1.0f), rectangle);
        }
        #endregion

        #endregion

        #region Filling Methods
        /// <summary>
        /// Fills an arc
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="region"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public void FillArc(IBrush brush, Math.Rectangle region, float startAngle, float sweepAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Fills a circle
        /// </summary>
        /// <param name="brush">The brush</param>
        /// <param name="position">The center point of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        public void FillCircle(IBrush brush, Math.Vector2 position, float radius)
        {
            SharpDXBrush sdxBrush = brush as SharpDXBrush;
            if (sdxBrush == null)
            {
                throw new ArgumentException("Argument has to be a SharpDXBrush", "brush");
            }

            Ellipse circle = new Ellipse(SharpDXHelper.CreateDrawingPoint(position + this._renderManager.ScreenOffset), radius, radius);
            SharpDXHelper.RenderTarget.FillEllipse(circle, sdxBrush.Brush);
        }
        /// <summary>
        /// Fills an ellipse
        /// </summary>
        /// <param name="brush">The brush</param>
        /// <param name="region">Size of the ellipse</param>
        public void FillEllipse(IBrush brush, Math.Rectangle region)
        {
            SharpDXBrush sdxBrush = brush as SharpDXBrush;
            if (sdxBrush == null)
            {
                throw new ArgumentException("Argument has to be a SharpDXBrush", "brush");
            }

            Ellipse ellipse = new Ellipse(SharpDXHelper.CreateDrawingPoint(region.Center + this._renderManager.ScreenOffset), region.Width / 2, region.Height / 2);
            SharpDXHelper.RenderTarget.FillEllipse(ellipse, sdxBrush.Brush);
        }
        /// <summary>
        /// Fills a pie
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="region"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweetAngle"></param>
        public void FillPie(IBrush brush, Math.Rectangle region, float startAngle, float sweetAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Fills a polygon
        /// </summary>
        /// <param name="brush">The brush</param>
        /// <param name="points">Polygon points</param>
        public void FillPolygon(IBrush brush, Math.Vector2[] points)
        {
            SharpDXBrush sdxBrush = brush as SharpDXBrush;
            if (brush == null)
            {
                throw new ArgumentException("Argument has to be SharpDXBrush", "brush");
            }

            if (points.Length != 0)
            {
                PathGeometry geometry = new PathGeometry(SharpDXHelper.Factory2D);
                using (GeometrySink sink = geometry.Open())
                {
                    sink.SetFillMode(FillMode.Winding);
                    sink.BeginFigure(SharpDXHelper.CreateDrawingPoint(points[points.Length - 1]), FigureBegin.Hollow);

                    for (int i = 0; i < points.Length; i++)
                    {
                        sink.AddLine(SharpDXHelper.CreateDrawingPoint(points[i]));
                    }

                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }

                SharpDXHelper.RenderTarget.FillGeometry(geometry, sdxBrush.Brush);
            }
        }
        /// <summary>
        /// Fills a rectangle
        /// </summary>
        /// <param name="brush">The brush</param>
        /// <param name="rectangle">Rectangle area</param>
        public void FillRectangle(IBrush brush, Math.Rectangle rectangle)
        {
            SharpDXBrush sdxBrush = brush as SharpDXBrush;
            if (sdxBrush == null)
            {
                throw new ArgumentException("Argument has to be a SharpDXBrush", "brush");
            }

            SharpDXHelper.RenderTarget.FillRectangle(SharpDXHelper.ConvertRectangle(rectangle + this._renderManager.ScreenOffset), sdxBrush.Brush);
        }
        #endregion
    }
}
