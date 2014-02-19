namespace Xemio.GameLibrary.Game.Handlers
{
    public interface ISortableTickHandler : ITickHandler
    {
        /// <summary>
        /// Gets the index of the tick. Default: 0.
        /// </summary>
        int TickIndex { get; }
    }
}
