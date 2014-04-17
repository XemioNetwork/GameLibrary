using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities.Components
{
    public interface IPositionModifier
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Offset { get; }
    }
}
