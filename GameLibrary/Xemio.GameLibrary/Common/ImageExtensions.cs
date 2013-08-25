using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Creates a stream with the content of the image.
        /// </summary>
        /// <param name="image">The image.</param>
        public static MemoryStream ToStream(this Image image)
        {
            MemoryStream stream = new MemoryStream();

            image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
