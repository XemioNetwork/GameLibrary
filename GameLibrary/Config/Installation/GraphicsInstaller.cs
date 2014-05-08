using System;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Config.Dependencies;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Logging;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Initialization;

namespace Xemio.GameLibrary.Config.Installation
{
    public class GraphicsInstaller : AbstractInstaller
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsInstaller"/> class.
        /// </summary>
        public GraphicsInstaller()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsInstaller"/> class.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public GraphicsInstaller(IInitializer initializer) : this(initializer, InterpolationMode.Bicubic, SmoothingMode.AntiAliased)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsInstaller"/> class.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <param name="interpolation">The interpolation.</param>
        /// <param name="smoothing">The smoothing.</param>
        public GraphicsInstaller(IInitializer initializer, InterpolationMode interpolation, SmoothingMode smoothing)
        {
            this.Initializer = initializer;
            this.InterpolationMode = interpolation;
            this.SmoothingMode = smoothing;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the initializer.
        /// </summary>
        public IInitializer Initializer { get; set; }
        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }
        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// Gets or sets the back buffer size.
        /// </summary>
        public Vector2 BackBufferSize { get; set; }
        #endregion

        #region Implementation of IInstaller
        /// <summary>
        /// Sets up the dependencies to other installers.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public override void SetupDependencies(DependencyManager manager)
        {
            manager.Dependency(() => new PluginInstaller());
        }
        /// <summary>
        /// Installs this instance.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="catalog">The catalog.</param>
        public override void Install(Configuration configuration, IComponentCatalog catalog)
        {
            logger.Info("Initializing graphics with {0}.", this.Initializer.Id);

            if (!this.Initializer.IsAvailable())
            {
                throw new InvalidOperationException("The selected graphics initializer is unavailable. " +
                    "Make sure your PC supports the selected graphics engine.");
            }
            
            if (this.Initializer.Factory == null)
            {
                throw new InvalidOperationException("Graphics initializer '" + this.Initializer.Id + "' does not provide a graphics factory.");
            }

            IRenderManager renderManager = this.Initializer.Factory.CreateRenderManager();
            IRenderFactory renderFactory = this.Initializer.Factory.CreateRenderFactory();
            ITextRasterizer rasterizer = this.Initializer.Factory.CreateTextRasterizer();

            var graphicsDevice = new GraphicsDevice(
                this.Initializer.DisplayName,
                new DisplayMode(this.BackBufferSize),
                renderManager, renderFactory, rasterizer);

            this.Initializer.Initialize(graphicsDevice);

            var implementations = catalog.Require<IImplementationManager>();
            implementations.Add<Type, IContentReader>(this.Initializer.Factory.CreateTextureReader());
            implementations.Add<Type, IContentWriter>(this.Initializer.Factory.CreateTextureWriter());

            foreach (IEffectProcessor processor in this.Initializer.Factory.CreateEffectProcessors())
            {
                implementations.Add<Type, IEffectProcessor>(processor);
            }
            
            catalog.Install(graphicsDevice);
        }
        #endregion
    }
}
