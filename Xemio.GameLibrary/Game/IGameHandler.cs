using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Game
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
        /// <param name="elapsed">The elapsed.</param>
        void Render(float elapsed);
    }
}
