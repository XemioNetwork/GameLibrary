using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullFont : IFont
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NullFont"/> class.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        public NullFont(string fontFamily, float size)
        {
            this.FontFamily = fontFamily;
            this.Size = size;
        }
        #endregion

        #region Implementation of IFont
        /// <summary>
        /// Gets the font family.
        /// </summary>
        public string FontFamily { get; private set; }
        /// <summary>
        /// Gets the size.
        /// </summary>
        public float Size { get; private set; }
        /// <summary>
        /// Measures the string.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2 MeasureString(string value)
        {
            return Vector2.Zero;
        }
        #endregion
    }
}
