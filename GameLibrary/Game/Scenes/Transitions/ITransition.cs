using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Rendering;

namespace Xemio.GameLibrary.Game.Scenes.Transitions
{
    public interface ITransition
    {
        /// <summary>
        /// Gets a value indicating whether the transition is completed.
        /// </summary>
        bool IsCompleted { get; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(float elapsed);
        /// <summary>
        /// Enters the transition.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="next">The next.</param>
        void Enter(Scene current, Scene next);
        /// <summary>
        /// Leaves the transition. NOTE: Current contains now the scene, that was declared as next before.
        /// </summary>
        /// <param name="current">The current.</param>
        void Leave(Scene current);
        /// <summary>
        /// Renders a transition between the specified scenes.
        /// </summary>
        /// <param name="current">The current scenes frame.</param>
        /// <param name="next">The next scenes frame.</param>
        void Render(IRenderTarget current, IRenderTarget next);
    }
}
