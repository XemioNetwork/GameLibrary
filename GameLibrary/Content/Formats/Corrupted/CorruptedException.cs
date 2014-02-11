using System;

namespace Xemio.GameLibrary.Content.Formats.Corrupted
{
    public class CorruptedException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CorruptedException" /> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public CorruptedException(Exception innerException)
            : base("The original format could not be initialized. Make sure your file format is not corrupted.", innerException)
        {
        }
        #endregion
    }
}
