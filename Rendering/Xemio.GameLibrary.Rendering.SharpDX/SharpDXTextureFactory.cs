using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct2D1;

using Bitmap = System.Drawing.Bitmap;
using D2DBitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    internal class SharpDXTextureFactory : ITextureFactory
    {
        #region Methods
        /// <summary>
        /// Loads a bitmap from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        private D2DBitmap LoadFromStream(Stream stream)
        {
            // Loads from file using System.Drawing.Image
            using (var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream))
            {
                var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
                var size = new DrawingSize(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte b = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte g = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte r = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte a = Marshal.ReadByte(bitmapData.Scan0, offset++);

                            int rgba = r | (g << 8) | (b << 16) | (a << 24);
                            tempStream.Write(rgba);
                        }

                    }

                    bitmap.UnlockBits(bitmapData);
                    tempStream.Position = 0;

                    return new D2DBitmap(SharpDXHelper.RenderTarget, size, tempStream, stride, bitmapProperties);
                }
            }
        }
        #endregion

        #region Implementation of ITextureFactory
        /// <summary>
        /// Creates a new texture.
        /// </summary>
        /// <param name="data">The binary texture data.</param>
        public ITexture CreateTexture(byte[] data)
        {
            return this.CreateTexture(new MemoryStream(data));
        }
        /// <summary>
        /// Creates a new texture.
        /// </summary>
        /// <param name="name">The resource filename.</param>
        /// <param name="resourceManager">The resource manager.</param>
        public ITexture CreateTexture(string name, System.Resources.ResourceManager resourceManager)
        {
            Bitmap bitmap = (Bitmap)resourceManager.GetObject(name);

            MemoryStream stream = new MemoryStream();
            if (bitmap != null) bitmap.Save(stream, ImageFormat.Png);

            return this.CreateTexture(stream);
        }
        /// <summary>
        /// Creates a new texture.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public ITexture CreateTexture(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("The file '" + fileName + "' does not exist.");
            }

            using (FileStream stream = File.OpenRead(fileName))
            {
                return this.CreateTexture(stream);
            }
        }
        /// <summary>
        /// Creates a new texture.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ITexture CreateTexture(System.IO.Stream stream)
        {
            return new SharpDXTexture(this.LoadFromStream(stream));
        }
        /// <summary>
        /// Creates a render target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IRenderTarget CreateRenderTarget(int width, int height)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
