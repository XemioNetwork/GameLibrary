using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.SDL
{
    using Drawing = System.Drawing;

    public static class SDLHelper
    {
        #region Methods
        /// <summary>
        /// Converts the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static Drawing.Color Convert(Color color)
        {
            return Drawing.Color.FromArgb(
                color.A, color.R, color.G, color.B);
        }
        /// <summary>
        /// Converts the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static Drawing.Point Convert(Vector2 vector)
        {
            return new Drawing.Point((int)vector.X, (int)vector.Y);
        }
        /// <summary>
        /// Converts the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Drawing.Rectangle Convert(Rectangle rect)
        {
            return new Drawing.Rectangle(
                (int)rect.X,
                (int)rect.Y,
                (int)rect.Width,
                (int)rect.Height);
        }
        #endregion
    }
}
