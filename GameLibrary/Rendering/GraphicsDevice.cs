using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Xemio.GameLibrary.Rendering.Events;
using Xemio.GameLibrary.Math;
using System.Windows.Forms;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Initialization;
using Xemio.GameLibrary.Rendering.Surfaces;

namespace Xemio.GameLibrary.Rendering
{
    [Require(typeof(IEventManager))]
    [Require(typeof(IImplementationManager))]

    public class GraphicsDevice : IConstructable
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDevice"/> class.
        /// </summary>
        public GraphicsDevice()
        {
            this._targets = new Stack<IRenderTarget>();
        }
        #endregion

        #region Fields
        private readonly Stack<IRenderTarget> _targets;
        private DisplayMode _displayMode;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Gets the scale.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                var surface = XGL.Components.Require<ISurface>();

                if (surface == null)
                {
                    return Vector2.Zero;
                }

                float x = surface.Width / (float)this.DisplayMode.Width;
                float y = surface.Height / (float)this.DisplayMode.Height;

                return new Vector2(x, y);
            }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager { get; private set; }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory RenderFactory { get; private set; }
        /// <summary>
        /// Gets the text rasterizer.
        /// </summary>
        public ITextRasterizer TextRasterizer { get; private set; }
        /// <summary>
        /// Gets the display mode.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get { return this._displayMode; }
            set
            {
                logger.Trace("Changing display mode to " + value.Width + "x" + value.Height);

                if (this._displayMode != null)
                {
                    this.CreateBackBuffer(value);
                }

                this._displayMode = value;

                var eventManager = XGL.Components.Require<IEventManager>();
                eventManager.Publish(new ResolutionChangedEvent(this._displayMode));
            }
        }
        /// <summary>
        /// Gets the back buffer.
        /// </summary>
        public IRenderTarget BackBuffer { get; private set; }
        /// <summary>
        /// Gets the current render target.
        /// </summary>
        public IRenderTarget RenderTarget
        {
            get
            {
                if (this._targets.Count > 0)
                    return this._targets.First();

                return this.BackBuffer;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates the back buffer.
        /// </summary>
        /// <param name="displayMode">The display mode.</param>
        private void CreateBackBuffer(DisplayMode displayMode)
        {
            logger.Trace("Creating back buffer.");

            this.BackBuffer = this.RenderFactory.CreateTarget(
                displayMode.Width,
                displayMode.Height);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the graphics device.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public void Initialize(IGraphicsInitializer initializer)
        {
            if (initializer == null)
            {
                throw new ArgumentNullException("initializer", @"No graphics initializer defined.");
            }

            initializer.Initialize(this);

            this.DisplayName = initializer.DisplayName;

            if (initializer.Factory == null)
            {
                throw new InvalidOperationException("Graphics initializer '" + initializer.Id + "' does not provide a graphics factory.");
            }

            this.RenderManager = initializer.Factory.CreateRenderManager();
            this.RenderFactory = initializer.Factory.CreateRenderFactory();
            this.TextRasterizer = initializer.Factory.CreateTextRasterizer();

            var implementations = XGL.Components.Require<IImplementationManager>();
            implementations.Add<Type, IReader>(initializer.Factory.CreateTextureReader());
            implementations.Add<Type, IWriter>(initializer.Factory.CreateTextureWriter());

            foreach (IEffectProcessor processor in initializer.Factory.CreateEffectProcessors())
            {
                implementations.Add<Type, IEffectProcessor>(processor);
            }

            logger.Info("Initialized graphics device with initializer {0}", initializer.Id);
        }
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.RenderManager.Clear(color);
        }
        /// <summary>
        /// Renders to the specified render target.
        /// </summary>
        /// <param name="target">The target.</param>
        public IDisposable RenderTo(IRenderTarget target)
        {
            this._targets.Push(target);
            return new ActionDisposable(() => this._targets.Pop());
        }
        /// <summary>
        /// Presents all drawn data.
        /// </summary>
        public void Present()
        {
            this.RenderManager.Present();
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            this.CreateBackBuffer(this.DisplayMode);
        }
        #endregion
    }
}
