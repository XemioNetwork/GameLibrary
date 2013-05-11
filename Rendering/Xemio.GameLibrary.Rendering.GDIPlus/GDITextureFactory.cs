using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Resources;
using System.Runtime.InteropServices;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    using Color = Xemio.GameLibrary.Rendering.Color;

    public class GDITextureFactory : ITextureFactory
    {
        #region Methods
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        private ITexture CreateTexture(Bitmap bitmap)
        {
            Bitmap clone = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);

            using (Graphics g = Graphics.FromImage(clone))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }

            return new GDITexture(clone);
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
                string message = string.Format("Cannot find '{0}'.", fileName);

                throw new InvalidOperationException(message);
            }

            ITexture texture;
            using (FileStream stream = File.OpenRead(fileName))
            {
                texture = this.CreateTexture(stream);
            }

            return texture;
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <returns></returns>
        public ITexture CreateTexture(string name, ResourceManager resourceManager)
        {
            return this.CreateTexture(resourceManager.GetObject(name) as Bitmap);
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public ITexture CreateTexture(Stream stream)
        {
            return this.CreateTexture(Image.FromStream(stream) as Bitmap);
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
