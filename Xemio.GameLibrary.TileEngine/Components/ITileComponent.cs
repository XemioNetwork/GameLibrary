using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.TileEngine.Components
{
    public interface ITileComponent
    {
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(Field tile, float elapsed);
        /// <summary>
        /// Renders the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        void Render(Field tile);
    }
}
