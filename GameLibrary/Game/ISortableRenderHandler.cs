using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Game
{
    public interface ISortableRenderHandler : IRenderHandler
    {
        /// <summary>
        /// Gets the index of the render. Default: 0.
        /// </summary>
        int RenderIndex { get; }
    }
}
