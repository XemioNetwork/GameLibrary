using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input.Events.Mouse
{
    public class MouseMoveEvent : MouseEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseDownEvent"/> class.
        /// </summary>
        /// <param name="button">The position.</param>
        /// <param name="button">The button.</param>
        public MouseMoveEvent(Vector2 position, MouseButtons button) : base(position, button)
        {
        }
        #endregion
    }
}
