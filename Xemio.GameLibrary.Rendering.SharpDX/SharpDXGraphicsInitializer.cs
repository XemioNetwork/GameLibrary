using System;
using System.Diagnostics;

namespace Xemio.GameLibrary.Rendering.SharpDX
{
    public class SharpDXGraphicsInitializer : IGraphicsInitializer
    {
        #region Fields
        private IGraphicsProvider _provider;
        #endregion

        #region Methods
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
        /// <returns></returns>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            return new SharpDXGraphicsProvider(graphicsDevice);
        }
        #endregion
    }
}
