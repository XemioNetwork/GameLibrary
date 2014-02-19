namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullTexture : IRenderTarget
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NullTexture"/> class.
        /// </summary>
        public NullTexture() : this(0, 0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NullTexture"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public NullTexture(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        #endregion

        #region Implementation of ITexture
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        public byte[] GetData()
        {
            return new byte[] {};
        }
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetData(byte[] data)
        {
        }
        #endregion
    }
}
