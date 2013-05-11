using SharpDX.Direct2D1;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.SharpDX.Geometry
{
    internal class SharpDXPen : IPen
    {
        #region Constructor
        /// <summary>
        /// Initializes the SharpDXPen
        /// </summary>
        /// <param name="color">Color of pen</param>
        /// <param name="thickness">Thickness of pen</param>
        public SharpDXPen(Color color, float thickness)
        {
            this.Color = color;
            this.Thickness = thickness;

            this.Brush = new SolidColorBrush(
                SharpDXHelper.RenderTarget,
                SharpDXHelper.CreateColor(this.Color));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Color of pen
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Thickness of pen
        /// </summary>
        public float Thickness { get; private set; }
        /// <summary>
        /// Brush of the pen
        /// </summary>
        public Brush Brush { get; private set; }
        #endregion
    }
}
