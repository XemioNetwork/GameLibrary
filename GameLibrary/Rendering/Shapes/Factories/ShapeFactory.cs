﻿using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        #region Implementation of IShapeFactory
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle)
        {
            return new RectangleShape { Region = rectangle };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background)
        {
            return new RectangleShape { Region = rectangle, Background = background };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outline">The outline.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IPen outline)
        {
            return new RectangleShape { Region = rectangle, Outline = outline };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background, IPen outline)
        {
            return new RectangleShape { Region = rectangle, Background = background, Outline = outline };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, float cornerRadius)
        {
            return new RectangleShape { Region = rectangle, CornerRadius = cornerRadius };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background, float cornerRadius)
        {
            return new RectangleShape { Region = rectangle, Background = background, CornerRadius = cornerRadius };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outline">The outline.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IPen outline, float cornerRadius)
        {
            return new RectangleShape { Region = rectangle, Outline = outline, CornerRadius = cornerRadius };
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background, IPen outline, float cornerRadius)
        {
            return new RectangleShape { Region = rectangle, Background = background, Outline = outline, CornerRadius = cornerRadius };
        }
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        public EllipseShape CreateEllipse(Rectangle region)
        {
            return new EllipseShape { Region = region };
        }
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="background">The background.</param>
        public EllipseShape CreateEllipse(Rectangle region, IBrush background)
        {
            return new EllipseShape { Region = region, Background = background };
        }
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="outline">The outline.</param>
        public EllipseShape CreateEllipse(Rectangle region, IPen outline)
        {
            return new EllipseShape { Region = region, Outline = outline };
        }
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        public EllipseShape CreateEllipse(Rectangle region, IBrush background, IPen outline)
        {
            return new EllipseShape { Region = region, Background = background, Outline = outline };
        }
        /// <summary>
        /// Creates an arc.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public ArcShape CreateArc(Rectangle region, float startAngle, float sweepAngle)
        {
            return new ArcShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle };
        }
        /// <summary>
        /// Creates an arc.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="outline">The outline.</param>
        public ArcShape CreateArc(Rectangle region, float startAngle, float sweepAngle, IPen outline)
        {
            return new ArcShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle, Outline = outline };
        }
        /// <summary>
        /// Creates a line.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        public LineShape CreateLine(Vector2 position, Vector2 direction)
        {
            return new LineShape { Position = position, Direction = direction };
        }
        /// <summary>
        /// Creates a line.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="outline">The outline.</param>
        public LineShape CreateLine(Vector2 position, Vector2 direction, IPen outline)
        {
            return new LineShape { Position = position, Direction = direction, Outline = outline };
        }
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle)
        {
            return new PieShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle };
        }
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="background">The background.</param>
        public PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IBrush background)
        {
            return new PieShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle, Background = background };
        }
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="outline">The outline.</param>
        public PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IPen outline)
        {
            return new PieShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle, Outline = outline };
        }
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        public PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IBrush background, IPen outline)
        {
            return new PieShape { Region = region, StartAngle = startAngle, SweepAngle = sweepAngle, Background = background, Outline = outline };
        }
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        public PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices)
        {
            return new PolygonShape { Position = position, Vertices = vertices };
        }
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="background">The background.</param>
        public PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IBrush background)
        {
            return new PolygonShape { Position = position, Vertices = vertices, Background = background };
        }
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outline">The outline.</param>
        public PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IPen outline)
        {
            return new PolygonShape { Position = position, Vertices = vertices, Outline = outline };
        }
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        public PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IBrush background, IPen outline)
        {
            return new PolygonShape { Position = position, Vertices = vertices, Background = background, Outline = outline };
        }
        #endregion
    }
}
