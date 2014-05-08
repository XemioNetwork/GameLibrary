namespace Xemio.GameLibrary.Game.Subscribers
{
    public interface ISortableTickSubscriber : ITickSubscriber
    {
        /// <summary>
        /// Gets the index of the tick. Default: 0.
        /// </summary>
        int TickIndex { get; }
    }
}
