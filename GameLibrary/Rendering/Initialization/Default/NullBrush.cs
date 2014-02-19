namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullBrush : IBrush
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NullBrush"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public NullBrush(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        #endregion

        #region Implementation of IBrush
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion
    }
}
