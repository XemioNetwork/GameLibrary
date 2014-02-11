using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Formats;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.GameLibrary.Rendering.Serialization
{
    public class SpriteSheetSerializer : Serializer<SpriteSheet>
    {
        #region Overrides of Serializer<SpriteSheet>
        /// <summary>
        /// Reads a sprite sheet.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override SpriteSheet Read(IFormatReader reader)
        {
            int frameWidth = reader.ReadInteger("FrameWidth");
            int frameHeight = reader.ReadInteger("FrameHeight");

            string fileName = reader.ReadString("Texture");

            var contentManager = XGL.Components.Require<ContentManager>();
            var texture = contentManager.Query<ITexture>(fileName);

            return new SpriteSheet(texture, frameWidth, frameHeight);
        }
        /// <summary>
        /// Writes the specified sprite sheet.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="sheet">The sheet.</param>
        /// <exception cref="System.InvalidOperationException">The sprite sheet does not exist inside the file cache. Please load or save it first using the content manager.</exception>
        public override void Write(IFormatWriter writer, SpriteSheet sheet)
        {
            var contentManager = XGL.Components.Require<ContentManager>();

            writer.WriteInteger("FrameWidth", sheet.FrameWidth);
            writer.WriteInteger("FrameHeight", sheet.FrameHeight);

            writer.WriteString("Texture", contentManager.GetFileName(sheet.Texture));
        }
        #endregion
    }
}
