using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Rendering.Xna
{
    public class XnaGraphicsInitializer : IGraphicsInitializer
    {
        #region Constructors
        public XnaGraphicsInitializer()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region IGraphicsInitializer Member
        /// <summary>
        /// Creates the graphics provider.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public IGraphicsProvider CreateProvider(GraphicsDevice graphicsDevice)
        {
            return new XnaGraphicsProvider(graphicsDevice);
        }
        #endregion
    }
}
