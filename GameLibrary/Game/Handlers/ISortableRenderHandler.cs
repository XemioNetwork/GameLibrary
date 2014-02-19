namespace Xemio.GameLibrary.Game.Handlers
{
    public interface ISortableRenderHandler : IRenderHandler
    {
        /// <summary>
        /// Gets the index of the render. Default: 0.
        /// </summary>
        int RenderIndex { get; }
    }
}
