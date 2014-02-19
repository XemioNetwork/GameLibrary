using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullTextRasterizer : ITextRasterizer
    {
        #region Implementation of ITextRasterizer
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        public void Render(IFont font, string text, Vector2 position)
        {
        }
        #endregion
    }
}
