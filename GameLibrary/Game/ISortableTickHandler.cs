using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Game
{
    public interface ISortableTickHandler : ITickHandler
    {
        /// <summary>
        /// Gets the index of the tick. Default: 0.
        /// </summary>
        int TickIndex { get; }
    }
}
