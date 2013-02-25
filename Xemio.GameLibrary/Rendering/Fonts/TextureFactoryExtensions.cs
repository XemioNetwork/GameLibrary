using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Fonts
{
    public static class TextureFactoryExtensions
    {
        #region Methods
        /// <summary>
        /// Creates a sprite font.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="fileName">Name of the file.</param>
        public static SpriteFont CreateSpriteFont(this ITextureFactory factory, string fileName)
        {
            return SpriteFont.Load(factory, fileName);
        }
        /// <summary>
        /// Creates a sprite font.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="stream">The stream.</param>
        public static SpriteFont CreateSpriteFont(this ITextureFactory factory, Stream stream)
        {
            return SpriteFont.Load(factory, stream);
        }
        #endregion
    }
}
