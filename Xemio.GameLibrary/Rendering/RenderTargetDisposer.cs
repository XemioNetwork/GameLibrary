using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering
{
    public class RenderTargetDisposer : IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderTargetDisposer"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public RenderTargetDisposer(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
        #endregion
        
        #region Implementation of IDisposable
        /// <summary>
        /// Disposes all resources and removes the render target from the render stack.
        /// </summary>
        public void Dispose()
        {
            this.GraphicsDevice.Targets.Pop();
        }
        #endregion
    }
}
