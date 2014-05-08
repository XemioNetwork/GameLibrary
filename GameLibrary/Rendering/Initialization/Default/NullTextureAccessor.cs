using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullTextureAccessor : ITextureAccessor
    {
        #region Implementation of ITextureAccessor
        /// <summary>
        /// Sets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="color">The color.</param>
        public void SetPixel(int x, int y, Color color)
        {
        }
        /// <summary>
        /// Gets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Color GetPixel(int x, int y)
        {
            return Color.Black;
        }
        /// <summary>
        /// Sets the texture data to the specified color values.
        /// </summary>
        /// <param name="colors">The colors.</param>
        public void SetData(Color[] colors)
        {
        }
        /// <summary>
        /// Gets the data as a color array.
        /// </summary>
        public Color[] GetData()
        {
            return new Color[] { };
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
