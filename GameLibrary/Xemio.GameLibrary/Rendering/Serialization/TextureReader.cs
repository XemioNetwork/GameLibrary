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
    public abstract class TextureReader : Reader<ITexture>
    {
        #region Overrides of Reader<ITexture>
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract override ITexture Read(IFormatReader reader);
        #endregion
    }
}
