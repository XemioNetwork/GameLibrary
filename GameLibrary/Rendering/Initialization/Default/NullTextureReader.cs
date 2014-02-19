using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Serialization;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullTextureReader : TextureReader
    {
        #region Overrides of TextureReader
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ITexture Read(IFormatReader reader)
        {
            return new NullTexture();
        }
        #endregion
    }
}
