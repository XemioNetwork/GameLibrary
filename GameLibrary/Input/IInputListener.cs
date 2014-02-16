using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Input
{
    public interface IInputListener
    {
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        int PlayerIndex { get; }
        /// <summary>
        /// Called when the input listener was attached to the player.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        void Attach(int playerIndex);
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        void Detach();
    }
}
