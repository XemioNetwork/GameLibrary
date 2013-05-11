using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaGraphicsInitializer : IGraphicsInitializer
    {
        #region Fields
        private IGraphicsProvider _provider;
        #endregion
        
        #region IGraphicsInitializer Member
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            bool isAvailable = false;
            try
            {
                GraphicsDevice graphicsDevice = XGL.GetComponent<GraphicsDevice>();
                this._provider = this.CreateProvider(graphicsDevice);

                isAvailable = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return isAvailable;
        }
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            if (this._provider != null)
            {
                return this._provider;
            }

            return new XnaGraphicsProvider(graphicsDevice);
        }
        #endregion
    }
}
