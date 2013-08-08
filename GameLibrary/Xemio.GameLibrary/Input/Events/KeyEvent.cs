using Xemio.GameLibrary.Input.Keyboard;

namespace Xemio.GameLibrary.Input.Events
{
    public class KeyEvent : IInputEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyUpEvent" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The new state.</param>
        /// <param name="playerIndex">The player index.</param>
        public KeyEvent(Keys key, InputState state, int playerIndex)
        {
            this.Key = key;
            this.State = state;
            this.PlayerIndex = playerIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key.
        /// </summary>
        public Keys Key { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public InputState State { get; private set; }
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        public int PlayerIndex { get; set; }
        #endregion

        #region Implementation of IInputEvent
        /// <summary>
        /// Applies this event to the specified player input.
        /// </summary>
        /// <param name="playerInput">The player input.</param>
        public void Apply(PlayerInput playerInput)
        {
            playerInput.Keyboard.SetState(this.Key, this.State);
        }
        #endregion Implementation of IInputEvent
    }
}
