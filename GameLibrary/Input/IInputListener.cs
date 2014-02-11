using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Input
{
    public interface IInputListener
    {
        /// <summary>
        /// Gets or sets the index of the player.
        /// </summary>
        int? PlayerIndex { get; set; }
        /// <summary>
        /// Called when the input listener was attached to the player.
        /// </summary>
        void OnAttached();
        /// <summary>
        /// Called when the input listener was detached from the player.
        /// </summary>
        void OnDetached();
    }
}
