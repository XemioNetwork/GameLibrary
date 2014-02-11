using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Components
{
    public interface IPositionComponent
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Offset { get; }
    }
}
