using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Plugins;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.GdiPlus.Serialization;
using Xemio.GameLibrary.Rendering.GdiPlus.Geometry;
using Xemio.GameLibrary.Rendering.Initialization;

namespace Xemio.GameLibrary.Rendering.GdiPlus
{
    public class GdiInitializer : IInitializer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiInitializer"/> class.
        /// </summary>
        public GdiInitializer()
        {
            this.Factory = new GdiInitializationFactory(this);
        }
        #endregion

        #region Implementation of IGraphicsInitializer
        /// <summary>
        /// Determines whether this instance is available.
        /// </summary>
        public bool IsAvailable()
        {
            return true;
        }
        /// <summary>
        /// Initializes the specified graphics device.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
        }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return "GDI+"; }
        }
        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public IInitializationFactory Factory { get; private set; }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return "gdiplus"; }
        }
        #endregion
    }
}
