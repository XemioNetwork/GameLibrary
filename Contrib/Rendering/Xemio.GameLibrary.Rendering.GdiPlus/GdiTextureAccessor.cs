using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class GdiTextureAccessor : ITextureAccessor
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiTextureAccessor"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public GdiTextureAccessor(GdiTexture texture)
        {
            this.Texture = texture;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public GdiTexture Texture { get; private set; }
        #endregion

        #region Implementation of ITextureAccessor
        /// <summary>
        /// Sets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="color">The color.</param>
        public void SetPixel(int x, int y, Color color)
        {
            //TODO: Replace with lock bits, just a temporary solution.
            this.Texture.Bitmap.SetPixel(x, y, Gdi.Convert(color));
        }
        /// <summary>
        /// Gets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Color GetPixel(int x, int y)
        {
            //TODO: Replace with lock bits, just a temporary solution.
            return Gdi.Convert(this.Texture.Bitmap.GetPixel(x, y));
        }
        /// <summary>
        /// Sets the texture data to the specified color values.
        /// </summary>
        /// <param name="colors">The colors.</param>
        public void SetData(Color[] colors)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the data as a color array.
        /// </summary>
        public Color[] GetData()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implementation of IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
