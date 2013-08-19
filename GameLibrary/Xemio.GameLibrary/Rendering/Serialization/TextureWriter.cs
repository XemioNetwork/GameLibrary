using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Rendering.Serialization
{
    [ManuallyLinked]
    public abstract class TextureWriter : ContentWriter<ITexture>
    {
        #region Overrides of ContentWriter<ITexture>
        /// <summary>
        /// Writes the specified texture.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract override void Write(IFormatWriter writer, ITexture value);
        #endregion
    }
}
