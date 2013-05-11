using SharpDX.Direct2D1;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    internal class SharpDXTexture : ITexture
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpDXTexture"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public SharpDXTexture(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return (int)this.Bitmap.Size.Height; }
        }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return (int)this.Bitmap.Size.Width; }
        }
        /// <summary>
        /// Gets the internal SharpDX bitmap.
        /// </summary>
        public Bitmap Bitmap { get; private set; }
        #endregion
    }
}
