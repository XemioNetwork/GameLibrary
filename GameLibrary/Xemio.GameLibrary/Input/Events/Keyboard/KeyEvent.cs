namespace Xemio.GameLibrary.Input.Events.Keyboard
{
    public class KeyEvent : IInputEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyUpEvent"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public KeyEvent(Keys key)
        {
            this.Key = key;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key.
        /// </summary>
        public Keys Key { get; private set; }
        #endregion
    }
}
