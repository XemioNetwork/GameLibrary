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
        /// Gets or sets the current.
        /// </summary>
        Scene Current { get; set; }
        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        Scene Next { get; set; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(float elapsed);
        /// <summary>
        /// Renders a transition between the specified scenes.
        /// </summary>
        /// <param name="current">The current scenes frame.</param>
        /// <param name="next">The next scenes frame.</param>
        void Render(IRenderTarget current, IRenderTarget next);
    }
}
