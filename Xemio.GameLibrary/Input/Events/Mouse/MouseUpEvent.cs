using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input.Events.Mouse
{
    public class MouseUpEvent : MouseEvent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseDownEvent"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="button">The button.</param>
        public MouseUpEvent(Vector2 position, MouseButtons button) : base(position, button)
        {
        }
        #endregion
    }
}
