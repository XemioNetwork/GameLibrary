using SharpDX.Direct2D1;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.SharpDX.Geometry
{
    internal class SharpDXBrush : IBrush
    {
        #region Constructor
        /// <summary>
        /// Initializes the brush
        /// </summary>
        /// <param name="height">Height of the brush</param>
        /// <param name="width">Width of the brush</param>
        public SharpDXBrush(Brush brush, int height, int width)
        {
            this.Brush = brush;

            this.Height = height;
            this.Width = width;
        }
        #endregion

        #region Properties
        /// <summary>
        /// SharpDX Brush
        /// </summary>
        public Brush Brush { get; private set; }
        /// <summary>
        /// Height of the brush
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Width of the brush
        /// </summary>
        public int Width { get; private set; }
        #endregion
    }
}
