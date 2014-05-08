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
        /// Accesses this texture instance. Changed data will be applied after disposing the accessor.
        /// </summary>
        ITextureAccessor Access();
    }
}
