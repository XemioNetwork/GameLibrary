using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Drawing.Imaging;
using Microsoft.Xna.Framework.Graphics;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaTextureFactory : ITextureFactory
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTextureFactory"/> class.
        /// </summary>
        public XnaTextureFactory()
        {
        }
        #endregion

        #region ITextureFactory Member
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="fileName">The texture filename.</param>
        /// <returns></returns>
        public ITexture CreateTexture(string fileName)
        {
            if (!File.Exists(fileName))
            {
                string message = string.Format(
                    "No file at path '{0}' was found. \nCoudn't create texture.",
                    fileName);

                throw new FileNotFoundException(message);
            }

            using (FileStream stream = File.OpenRead(fileName))
            {
                return this.CreateTexture(stream);
            }
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="resourceManager">The resource manager.</param>
        public ITexture CreateTexture(string name, ResourceManager resourceManager)
        {
            Bitmap bitmap = (Bitmap)resourceManager.GetObject(name);

            Stream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);

            return this.CreateTexture(stream);
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public ITexture CreateTexture(Stream stream)
        {
            return new XnaTexture(Texture2D.FromStream(XnaHelper.GraphicsDevice, stream));
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="data">The binary texture data.</param>
        /// <returns></returns>
        public ITexture CreateTexture(byte[] data)
        {
            return this.CreateTexture(new MemoryStream(data));
        }
        #endregion
    }
}
