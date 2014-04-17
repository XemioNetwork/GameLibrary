using System;

namespace Xemio.GameLibrary.Content.Formats.None
{
    public class InvalidFormatException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatException" /> class.
        /// </summary>
        public InvalidFormatException()
            : base("Calls to Read or Write can not be made inside formatless serializers. Override the BypassFormat method to prevent this behavior.")
        {
        }
        #endregion
    }
}
