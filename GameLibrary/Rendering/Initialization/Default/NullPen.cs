namespace Xemio.GameLibrary.Rendering.Initialization.Default
{
    public class NullPen : IPen
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NullPen"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public NullPen(Color color, float thickness)
        {
            this.Color = color;
            this.Thickness = thickness;
        }
        #endregion

        #region Implementation of IPen
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }
        /// <summary>
        /// Gets the thickness.
        /// </summary>
        public float Thickness { get; private set; }
        #endregion
    }
}
