using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.Initialization
{
    public interface IGraphicsFactory
    {
        /// <summary>
        /// Gets the texture writer.
        /// </summary>
        IWriter CreateTextureWriter();
        /// <summary>
        /// Gets the texture reader.
        /// </summary>
        IReader CreateTextureReader();
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        IRenderManager CreateRenderManager();
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        IRenderFactory CreateRenderFactory();
        /// <summary>
        /// Gets the geometry manager.
        /// </summary>
        IGeometryManager CreateGeometryManager();
        /// <summary>
        /// Gets the geometry factory.
        /// </summary>
        IGeometryFactory CreateGeometryFactory();
    }
}
