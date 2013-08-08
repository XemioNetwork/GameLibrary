using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Input.Events
{
    /// <summary>
    /// Base interface for input events.
    /// </summary>
    public interface IInputEvent : IEvent
    {
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        int PlayerIndex { get; }

        /// <summary>
        /// Applies this event to the specified player input.
        /// </summary>
        /// <param name="playerInput">The player input.</param>
        void Apply(PlayerInput playerInput);
    }
}
