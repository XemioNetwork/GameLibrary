namespace Xemio.GameLibrary.Input.Events.Keyboard
{
    public class KeyUpEvent : KeyEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyUpEvent"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public KeyUpEvent(Keys key) : base(key)
        {
        }
        #endregion
    }
}
