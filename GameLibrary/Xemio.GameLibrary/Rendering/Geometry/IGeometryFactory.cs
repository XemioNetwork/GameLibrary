using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    [AbstractComponent]
    public interface IGeometryFactory
    {
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        IPen CreatePen(Color color);
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        IPen CreatePen(Color color, float thickness);
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        IBrush CreateSolid(Color color);
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        IBrush CreateGradient(Color top, Color bottom, Vector2 from, Vector2 to);
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        IBrush CreateTexture(ITexture texture);
    }
}
