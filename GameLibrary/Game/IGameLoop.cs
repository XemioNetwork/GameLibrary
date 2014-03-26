using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Game.Handlers;

namespace Xemio.GameLibrary.Game
{
    [Abstraction]
    public interface IGameLoop : IComponent
    {
        /// <summary>
        /// Runs the game loop and creates a parallel thread.
        /// </summary>
        void Run();
        /// <summary>
        /// Stops the game loop.
        /// </summary>
        void Stop();
        /// <summary>
        /// Subscribes and adds the handler to the gameloop.
        /// </summary>
        /// <param name="handler">The handler.</param>
        void Subscribe(IGameHandler handler);
        /// <summary>
        /// Unsubscribes the gameloop and removes the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        void Unsubscribe(IGameHandler handler);
        /// <summary>
        /// Gets or sets the lag compensation. If set to ExecuteMissedTicks, the game loop will
        /// always run at the same tick frequency.
        /// </summary>
        LagCompensation LagCompensation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameLoop"/> is active.
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Gets the current frame index.
        /// </summary>
        long FrameIndex { get; }
        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        int FramesPerSecond { get; }
        /// <summary>
        /// Gets the frame time (render time + tick time).
        /// </summary>
        double FrameTime { get; }
        /// <summary>
        /// Gets the target frame time.
        /// </summary>
        double TargetFrameTime { get; set; }
        /// <summary>
        /// Gets the tick time.
        /// </summary>
        double TickTime { get; }
        /// <summary>
        /// Gets the render time.
        /// </summary>
        double RenderTime { get; }
    }
}
