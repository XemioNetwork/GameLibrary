using Xemio.GameLibrary.Math;

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
            return new RectangleShape {Region = rectangle};
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background)
        {
            return new RectangleShape {Region = rectangle, Background = background};
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outline">The outline.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IPen outline)
        {
            return new RectangleShape {Region = rectangle, Outline = outline};
        }
        /// <summary>
        /// Creates a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="background">The background.</param>
        /// <param name="outline">The outline.</param>
        public RectangleShape CreateRectangle(Rectangle rectangle, IBrush background, IPen outline)
        {
            return new RectangleShape {Region = rectangle, Background = background, Outline = outline};
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
        #endregion
    }
}
