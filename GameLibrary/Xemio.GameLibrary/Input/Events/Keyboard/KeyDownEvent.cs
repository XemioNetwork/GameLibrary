namespace Xemio.GameLibrary.Input.Events.Keyboard
{
    public class KeyDownEvent : KeyEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyUpEvent"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public KeyDownEvent(Keys key) : base(key)
        {
        }
        #endregion
    }
}
