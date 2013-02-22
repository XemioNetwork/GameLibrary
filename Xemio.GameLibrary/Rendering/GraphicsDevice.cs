using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering
{
    public class GraphicsDevice : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDevice"/> class.
        /// </summary>
        public GraphicsDevice(IntPtr handle)
        {
            this.Handle = handle;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public IGraphicsProvider Graphics { get; set; }
        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        public IGeometryProvider Geometry { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Presents all drawn data.
        /// </summary>
        public void Present()
        {
            this.Graphics.RenderManager.Present();
        }
        #endregion
    }
}
