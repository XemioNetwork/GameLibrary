namespace Xemio.GameLibrary.Game.Timing
{
    public interface IGameHandler
    {
        /// <summary>
        /// Handles game updates.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(float elapsed);
        /// <summary>
        /// Handles render calls.
        /// </summary>
        void Render();
    }
}
