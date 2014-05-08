namespace Xemio.GameLibrary.Game.Subscribers
{
    public interface ISortableRenderSubscriber : IRenderSubscriber
    {
        /// <summary>
        /// Gets the index of the render. Default: 0.
        /// </summary>
        int RenderIndex { get; }
    }
}
