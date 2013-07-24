using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xemio.GameLibrary.Math;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.GameLibrary.Rendering.SDL
{
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
        public static System.Drawing.Color Convert(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        /// <summary>
        /// Converts the specified rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        public static System.Drawing.Rectangle Convert(Rectangle rectangle)
        {
            return new System.Drawing.Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public static System.Drawing.Point[] Convert(Vector2[] vertices)
        {
            System.Drawing.Point[] points = new System.Drawing.Point[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                points[i] = new System.Drawing.Point((int)vertices[i].X, (int)vertices[i].Y);
            }

            return points;
        }
        /// <summary>
        /// Converts the specified vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="offset">The offset.</param>
        public static System.Drawing.Point[] Convert(Vector2[] vertices, Vector2 offset)
        {
            return GDIHelper.Convert(vertices.Select(v => v + offset).ToArray());
        }
        #endregion
    }
}
