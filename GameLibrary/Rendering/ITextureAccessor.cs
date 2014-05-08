using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Rendering
{
    public interface ITextureAccessor : IDisposable
    {
        /// <summary>
        /// Sets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="color">The color.</param>
        void SetPixel(int x, int y, Color color);
        /// <summary>
        /// Gets the pixel at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        Color GetPixel(int x, int y);
        /// <summary>
        /// Sets the texture data to the specified color values.
        /// </summary>
        /// <param name="colors">The colors.</param>
        void SetData(Color[] colors);
        /// <summary>
        /// Gets the data as a color array.
        /// </summary>
        Color[] GetData();
    }
}
