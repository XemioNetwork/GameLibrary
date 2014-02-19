namespace Xemio.GameLibrary.Game.Handlers
{
    public interface ITickHandler : IGameHandler
    {
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed time since last tick.</param>
        void Tick(float elapsed);
    }
}
