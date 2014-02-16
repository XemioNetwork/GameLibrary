using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Game
{
    public interface ITickHandler : IGameHandler
    {
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed time since last tick.</param>
        void Tick(float elapsed);
    }
}
