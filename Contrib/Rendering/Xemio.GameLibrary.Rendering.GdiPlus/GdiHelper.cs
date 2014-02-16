using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    using Drawing = System.Drawing;

    public static class GdiHelper
    {
        #region GDI Methods
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr pointer);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr pointer, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, GdiRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, GdiRasterOperations dwRop);
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
            var points = new Drawing.Point[vertices.Length];
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
            return GdiHelper.Convert(vertices.Select(v => v + offset).ToArray());
        }
        /// <summary>
        /// Converts the specified smoothing.
        /// </summary>
        /// <param name="smoothing">The smoothing.</param>
        public static Drawing.Drawing2D.SmoothingMode Convert(SmoothingMode smoothing)
        {
            switch (smoothing)
            {
                case SmoothingMode.None:
                    return Drawing.Drawing2D.SmoothingMode.None;
                case SmoothingMode.AntiAliased:
                    return Drawing.Drawing2D.SmoothingMode.HighQuality;
                default:
                    throw new ArgumentOutOfRangeException("smoothing");
            }
        }
        /// <summary>
        /// Converts the specified interpolation mode.
        /// </summary>
        /// <param name="interpolation">The interpolation.</param>
        public static Drawing.Drawing2D.InterpolationMode Convert(InterpolationMode interpolation)
        {
            switch (interpolation)
            {
                case InterpolationMode.NearestNeighbor:
                    return Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                case InterpolationMode.Linear:
                    return Drawing.Drawing2D.InterpolationMode.Bilinear;
                case InterpolationMode.Bicubic:
                    return Drawing.Drawing2D.InterpolationMode.Bicubic;
                default:
                    throw new ArgumentOutOfRangeException("interpolation");
            }
        }
        #endregion
    }
}
