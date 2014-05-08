namespace Xemio.GameLibrary.Game.Subscribers
{
    public interface ITickSubscriber : ISubscriber
    {
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed time since last tick.</param>
        void Tick(float elapsed);
    }
}
