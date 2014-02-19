using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullTextureWriter : TextureWriter
    {
        #region Overrides of TextureWriter
        /// <summary>
        /// Writes the specified texture.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, ITexture value)
        {
        }
        #endregion
    }
}
