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
    }
}
