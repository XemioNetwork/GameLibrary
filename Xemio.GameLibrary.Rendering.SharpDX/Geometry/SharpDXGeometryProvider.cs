using SharpDX.Direct2D1;
using System;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.SharpDX.Geometry
{
    internal class SharpDXGeometryProvider : IGeometryProvider
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

        #region Constants
        public const float ShiftAngle = 0.16726646f;
        #endregion

        #region Fields
        private SharpDXRenderManager _renderManager;
        #endregion

        #region Methods
        /// <summary>
        /// Stretches the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private Vector2 Stretch(Vector2 vector, float width, float height)
        {
            return new Vector2(vector.X * width, vector.Y * height);
        }
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
            int width = (int)(region.Width / 2);
            int height = (int)(region.Height / 2);

            Vector2 angleDirection = MathHelper.ToVector(startAngle);
            Vector2 stretchedDirection = this.Stretch(
                angleDirection, width, height);

            Vector2 position = new Vector2(region.X + width, region.Y + width);

            for (float i = 0; i < sweepAngle; i += ShiftAngle)
            {
                Vector2 start = Vector2.Rotate(stretchedDirection, i) + position;
                Vector2 end = Vector2.Rotate(stretchedDirection, i + ShiftAngle) + position;

                this.DrawLine(pen, start, end);
            }
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
            this.DrawArc(this.Factory.CreatePen(color), region, startAngle, sweepAngle);
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
            SharpDXHelper.RenderTarget.DrawEllipse(ellipse, SharpDXHelper.CreateBrushFromPen(pen), pen.Thickness);
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
            SharpDXHelper.RenderTarget.DrawEllipse(ellipse, SharpDXHelper.CreateBrushFromPen(pen), pen.Thickness);
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
                SharpDXHelper.CreateBrushFromPen(pen),
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
        /// <param name="sweepAngle">The sweet angle.</param>
        public void DrawPie(IPen pen, Math.Rectangle region, float startAngle, float sweepAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweet angle.</param>
        public void DrawPie(Color color, Math.Rectangle region, float startAngle, float sweepAngle)
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
                SharpDXHelper.CreateBrushFromPen(pen), 
                pen.Thickness);
        }
        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="color">Color of the stroke</param>
        /// <param name="rectangle">Rectangle</param>
        public void DrawRectangle(Color color, Math.Rectangle rectangle)
        {
            this.DrawRectangle(this.Factory.CreatePen(color), rectangle);
        }
        #endregion

        #region RoundedRectangle
        /// <summary>
        /// Draws the rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void DrawRoundedRectangle(IPen pen, Rectangle rectangle, float radius)
        {
            this.DrawArc(
                pen,
                new Rectangle(rectangle.X - 1, rectangle.Y - 1, radius * 2, radius * 2),
                MathHelper.ToRadians(-180),
                MathHelper.ToRadians(90));

            this.DrawArc(
                pen,
                new Rectangle(rectangle.X + rectangle.Width - radius * 2, rectangle.Y - 1, radius * 2, radius * 2),
                MathHelper.ToRadians(-90),
                MathHelper.ToRadians(90));

            this.DrawArc(
                pen,
                new Rectangle(rectangle.X + rectangle.Width - radius * 2, rectangle.Y + rectangle.Height - radius * 2, radius * 2, radius * 2),
                MathHelper.ToRadians(0),
                MathHelper.ToRadians(90));

            this.DrawArc(
                pen,
                new Rectangle(rectangle.X - 1, rectangle.Y + rectangle.Height - radius * 2, radius * 2, radius * 2),
                MathHelper.ToRadians(90),
                MathHelper.ToRadians(90));

            this.DrawLine(
                pen,
                new Vector2(rectangle.X + radius - 1, rectangle.Y),
                new Vector2(rectangle.X + rectangle.Width - radius, rectangle.Y));

            this.DrawLine(pen,
                new Vector2(rectangle.X + radius - 1, rectangle.Y + rectangle.Height),
                new Vector2(rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height));

            this.DrawLine(pen,
                new Vector2(rectangle.X, rectangle.Y + radius - 1),
                new Vector2(rectangle.X, rectangle.Y + rectangle.Height - radius));

            this.DrawLine(pen,
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + radius - 1),
                new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height - radius));
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

            Ellipse circle = new Ellipse(
                SharpDXHelper.CreateDrawingPoint(position + this._renderManager.ScreenOffset),
                radius, radius);

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
                throw new ArgumentException("Argument has to be a SharpDXBrush.", "brush");
            }

            Ellipse ellipse = new Ellipse(
                SharpDXHelper.CreateDrawingPoint(region.Center + this._renderManager.ScreenOffset),
                region.Width / 2,
                region.Height / 2);

            SharpDXHelper.RenderTarget.FillEllipse(ellipse, sdxBrush.Brush);
        }
        /// <summary>
        /// Fills a pie
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="region"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public void FillPie(IBrush brush, Math.Rectangle region, float startAngle, float sweepAngle)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="radius">The radius.</param>
        public void FillRoundedRectangle(IBrush brush, Rectangle rectangle, float radius)
        {
            SharpDXBrush sdxBrush = brush as SharpDXBrush;
            if (sdxBrush == null)
            {
                throw new ArgumentException("Argument has to be a SharpDXBrush.", "brush");
            }

            RoundedRectangle rounded = new RoundedRectangle
            {
                Rect = SharpDXHelper.ConvertRectangle(rectangle),
                RadiusX = radius,
                RadiusY = radius
            };

            SharpDXHelper.RenderTarget.FillRoundedRectangle(rounded, sdxBrush.Brush);
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
