using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.UI
{
    public interface IGraphics : IRenderManager, IGeometryProvider, IGeometryFactory
    {
    }
}
