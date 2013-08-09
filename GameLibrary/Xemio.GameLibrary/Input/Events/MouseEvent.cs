using Xemio.GameLibrary.Input.Mouse;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Input.Events
{
    public class MouseEvent : IInputEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseEvent" /> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="state">The state.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public MouseEvent(MouseButtons button, InputState state, int playerIndex)
        {
            this.Button = button;
            this.State = state;
            this.PlayerIndex = playerIndex;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the button.
        /// </summary>
        public MouseButtons Button { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public InputState State { get; private set; }
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        #endregion

        #region Implementation of IInputEvent
        /// <summary>
        /// Applies this event to the specified player input.
        /// </summary>
        /// <param name="playerInput">The player input.</param>
        public void Apply(PlayerInput playerInput)
        {
            playerInput.Mouse.SetState(this.Button, this.State);
        }
        #endregion Implementation of IInputEvent
    }
}
