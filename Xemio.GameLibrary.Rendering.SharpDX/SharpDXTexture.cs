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
            this._bitmap = bitmap;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Bitmap
        /// </summary>
        private Bitmap _bitmap;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return (int)this._bitmap.Size.Height; }
        }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return (int)this._bitmap.Size.Width; }
        }
        /// <summary>
        /// Gets the bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            get { return this._bitmap; }
        }
        #endregion
    }
}
