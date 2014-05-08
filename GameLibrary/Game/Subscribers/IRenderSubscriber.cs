namespace Xemio.GameLibrary.Game.Subscribers
{
    public interface IRenderSubscriber : ISubscriber
    {
        /// <summary>
        /// Handles render calls.
        /// </summary>
        void Render();
    }
}
