using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Resources;
using System.Text;
using System.IO;
using SdlDotNet.Graphics;

namespace Xemio.GameLibrary.Rendering.SDL
{
    public class SDLTextureFactory : ITextureFactory
    {
        #region Methods
        /// <summary>
        /// Creates the surface.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        private Surface CreateSurface(Bitmap bitmap)
        {
            Bitmap clone = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);

            using (Graphics g = Graphics.FromImage(clone))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }

            return new Surface(clone);

        }
        /// <summary>
        /// Creates a new texture.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        private SDLTexture CreateTexture(Bitmap bitmap)
        {
            return new SDLTexture(this.CreateSurface(bitmap));
        }
        #endregion

        #region Implementation of ITextureFactory
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="fileName">The texture filename.</param>
        public ITexture CreateTexture(string fileName)
        {
            return this.CreateTexture(new Bitmap(fileName));
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="resourceManager">The resource manager.</param>
        public ITexture CreateTexture(string name, ResourceManager resourceManager)
        {
            return this.CreateTexture(resourceManager.GetObject(name) as Bitmap);
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ITexture CreateTexture(Stream stream)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                return this.CreateTexture(new Bitmap(memory));
            }
        }
        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="data">The binary texture data.</param>
        public ITexture CreateTexture(byte[] data)
        {
            return this.CreateTexture(new Bitmap(new MemoryStream(data)));
        }
        /// <summary>
        /// Creates a render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IRenderTarget CreateRenderTarget(int width, int height)
        {
            return new SDLRenderTarget(width, height);
        }
        #endregion
    }
}
