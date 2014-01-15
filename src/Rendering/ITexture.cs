namespace Xemio.GameLibrary.Rendering
{
    public interface ITexture
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Gets the texture data.
        /// </summary>
        byte[] GetData();
        /// <summary>
        /// Sets the texture data.
        /// </summary>
        /// <param name="data">The data.</param>
        void SetData(byte[] data);
    }
}
