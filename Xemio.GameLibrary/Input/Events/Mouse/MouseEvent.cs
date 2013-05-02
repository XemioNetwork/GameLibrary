using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input.Events.Mouse
{
    public class MouseEvent : IInputEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEvent"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="button">The button.</param>
        public MouseEvent(Vector2 position, MouseButtons button)
        {
            this.Position = position;
            this.Button = button;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// Gets the button.
        /// </summary>
        public MouseButtons Button { get; private set; }
        #endregion
    }
}
