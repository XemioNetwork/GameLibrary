namespace Xemio.GameLibrary.Rendering.HTML5
{
    public static class HTMLHelper
    {
        #region Methods
        /// <summary>
        /// Converts the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        public static string Convert(Color color)
        {
            return "#" +
                   color.R.ToString("X") +
                   color.G.ToString("X") +
                   color.B.ToString("X") + 
                   color.A.ToString("X");
        }
        #endregion
    }
}