using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Config.Installers
{
    public class SurfaceInstaller : AbstractInstaller
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SurfaceInstaller"/> class.
        /// </summary>
        public SurfaceInstaller() : this(new NullSurface())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SurfaceInstaller"/> class.
        /// </summary>
        /// <param name="surface">The surface.</param>
        public SurfaceInstaller(ISurface surface)
        {
            this.Surface = surface;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the surface.
        /// </summary>
        public ISurface Surface { get; set; }
        #endregion

        #region Overrides of AbstractInstaller
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            catalog.Install(this.Surface);
        }
        #endregion
    }
}
