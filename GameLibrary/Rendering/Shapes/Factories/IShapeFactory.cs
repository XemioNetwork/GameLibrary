using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Shapes.Factories
{
    public interface IShapeFactory
    {
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        RectangleShape CreateRectangle(Rectangle rectangle);
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        RectangleShape CreateRectangle(Rectangle rectangle, IBrush background);
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outline">The outline.</param>
        RectangleShape CreateRectangle(Rectangle rectangle, IPen outline);
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        RectangleShape CreateRectangle(Rectangle rectangle, IBrush background, IPen outline);
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        EllipseShape CreateEllipse(Rectangle region);
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="background">The background.</param>
        EllipseShape CreateEllipse(Rectangle region, IBrush background);
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="outline">The outline.</param>
        EllipseShape CreateEllipse(Rectangle region, IPen outline);
        /// <summary>
        /// Creates an ellipse.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        EllipseShape CreateEllipse(Rectangle region, IBrush background, IPen outline);
        /// <summary>
        /// Creates an arc.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        ArcShape CreateArc(Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Creates an arc.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="outline">The outline.</param>
        ArcShape CreateArc(Rectangle region, float startAngle, float sweepAngle, IPen outline);
        /// <summary>
        /// Creates a line.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        LineShape CreateLine(Vector2 position, Vector2 direction);
        /// <summary>
        /// Creates a line.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="outline">The outline.</param>
        LineShape CreateLine(Vector2 position, Vector2 direction, IPen outline);
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="background">The background.</param>
        PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IBrush background);
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="outline">The outline.</param>
        PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IPen outline);
        /// <summary>
        /// Creates a pie.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        PieShape CreatePie(Rectangle region, float startAngle, float sweepAngle, IBrush background, IPen outline);
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices);
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="background">The background.</param>
        PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IBrush background);
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outline">The outline.</param>
        PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IPen outline);
        /// <summary>
        /// Creates a polygon.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        PolygonShape CreatePolygon(Vector2 position, Vector2[] vertices, IBrush background, IPen outline);
    }
}
