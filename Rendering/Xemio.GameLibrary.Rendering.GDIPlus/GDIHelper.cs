using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Xemio.GameLibrary.Rendering.GDIPlus
{
    using Drawing = System.Drawing;
using Xemio.GameLibrary.Math;

    public static class GDIHelper
    {
        #region GDI Methods
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, GDIRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, GDIRasterOperations dwRop);
        #endregion

        #region Methods
        /// <summary>
        /// Converts the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public static Drawing.Color Convert(Color color)
        {
            return Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        /// <summary>
        /// Converts the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static Drawing.Rectangle Convert(Rectangle rectangle)
        {
            return new Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public static Drawing.Point[] Convert(Vector2[] vertices)
        {
            Drawing.Point[] points = new Drawing.Point[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                points[i] = new Drawing.Point((int)vertices[i].X, (int)vertices[i].Y);
            }

            return points;
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="offset">The offset.</param>
        public static Drawing.Point[] Convert(Vector2[] vertices, Vector2 offset)
        {
            return GDIHelper.Convert(vertices.Select(v => v + offset).ToArray());
        }
        #endregion
    }
}
