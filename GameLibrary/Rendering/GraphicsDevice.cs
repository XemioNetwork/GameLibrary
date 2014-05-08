using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        /// Initializes a new instance of the <see cref="GraphicsDevice" /> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="displayMode">The display mode.</param>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="factory">The render factory.</param>
        /// <param name="rasterizer">The rasterizer.</param>
        public GraphicsDevice(string displayName, DisplayMode displayMode, IRenderManager renderManager, IRenderFactory factory, ITextRasterizer rasterizer)
        {
            this._targets = new Stack<IRenderTarget>();
            this._processors = new List<IEffectProcessor>();

            this.DisplayName = displayName;
            this.DisplayMode = displayMode;

            this.RenderManager = renderManager;
            this.TextRasterizer = rasterizer;

            this.Factory = factory;
            this.Features = GraphicsDeviceFeatures.EffectInterception;
        }
        #endregion

        #region Fields
        private readonly Stack<IRenderTarget> _targets;
        private readonly List<IEffectProcessor> _processors;
 
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
        /// Gets the text rasterizer.
        /// </summary>
        public ITextRasterizer TextRasterizer { get; private set; }
        /// <summary>
        /// Gets the render factory.
        /// </summary>
        public IRenderFactory Factory { get; private set; }
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
        /// Gets or sets the features.
        /// </summary>
        public GraphicsDeviceFeatures Features { get; set; }
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
        /// Intercepts the specified action.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="evt">The event.</param>
        private void Intercept<T>(Action<IEffectProcessor, T> action, T evt) where T : ICancelableEvent
        {
            if (this.HasFeature(GraphicsDeviceFeatures.EffectInterception))
            {
                foreach (IEffectProcessor processor in this._processors)
                {
                    action(processor, evt);
                    if (evt.IsCanceled)
                    {
                        break;
                    }
                }
            }
            if (this.HasFeature(GraphicsDeviceFeatures.EventPublishing))
            {
                var eventManager = XGL.Components.Require<IEventManager>();
                eventManager.Publish((IEvent)evt);
            }
        }
        /// <summary>
        /// Determines wether the graphics device should proceed with a specified action after processing effects.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="eventCreation">The event creation.</param>
        private bool InterceptAndShouldProceed<T>(Action<IEffectProcessor, T> action, Func<T> eventCreation) where T : ICancelableEvent
        {
            if (this.HasFeature(GraphicsDeviceFeatures.EffectInterception) ||
                this.HasFeature(GraphicsDeviceFeatures.EventPublishing))
            {
                var evt = eventCreation();
                this.Intercept(action, evt);

                return !evt.IsCanceled;
            }

            return true;
        }
        /// <summary>
        /// Creates the back buffer.
        /// </summary>
        /// <param name="displayMode">The display mode.</param>
        private void CreateBackBuffer(DisplayMode displayMode)
        {
            logger.Trace("Creating back buffer.");

            this.BackBuffer = this.Factory.CreateTarget(
                displayMode.Width,
                displayMode.Height);
        }
        #endregion

        #region Feature Methods
        /// <summary>
        /// Determines whether the specified feature is enabled.
        /// </summary>
        /// <param name="feature">The feature.</param>
        public bool HasFeature(GraphicsDeviceFeatures feature)
        {
            return this.Features.HasFlag(feature);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        public void Clear(Color color)
        {
            this.RenderManager.Clear(color);
        }
        /// <summary>
        /// Applies the specified effects.
        /// </summary>
        /// <param name="effects">The effects.</param>
        public IDisposable Apply(params IEffect[] effects)
        {
            var actions = new Stack<Action>();
            var implementations = XGL.Components.Get<IImplementationManager>();

            foreach (IEffect effect in effects)
            {
                //Access to foreach variable in closure could have different
                //behavior after compile time => prevented using a copy of the
                //current iterator variable
                var currentEffect = effect;

                var processor = implementations.Get<Type, IEffectProcessor>(effect.GetType());
                if (processor != null)
                {
                    processor.Enable(effect, this);
                    this._processors.Add(processor);

                    actions.Push(() =>
                    {
                        processor.Disable(currentEffect, this);
                        this._processors.Remove(processor);
                    });
                }

                var bundle = effect as EffectBundle;
                if (bundle != null)
                {
                    var bundleEffects = bundle.Effects.ToArray();
                    var disposable = this.Apply(bundleEffects);

                    actions.Push(disposable.Dispose);
                }
            }

            return new ActionDisposable(() =>
            {
                while (actions.Count > 0)
                {
                    actions.Pop().Invoke();
                }
            });
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
        /// Uses a render target to scissor out a specific region on the screen.
        /// </summary>
        /// <param name="region">The region.</param>
        public IDisposable RenderTo(Rectangle region)
        {
            IRenderTarget target;
            return this.RenderTo(region, out target);
        }
        /// <summary>
        /// Uses a render target to scissor out a specific region on the screen.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="renderTarget">The render target.</param>
        public IDisposable RenderTo(Rectangle region, out IRenderTarget renderTarget)
        {
            int width = (int)region.Width - (int)region.X;
            int height = (int)region.Height - (int)region.Y;

            IRenderTarget target = this.Factory.CreateTarget(width, height);

            IDisposable popRenderTarget = this.RenderTo(target);
            IDisposable blitTexture = new ActionDisposable(() => this.Render(target, region, new Rectangle(0, 0, region.Width, region.Height)));

            //Set the out parameter to the used target.
            renderTarget = target;

            return Disposable.Combine(popRenderTarget, blitTexture);
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        public void Render(ITexture texture, Vector2 position)
        {
            this.Render(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        public void Render(ITexture texture, Rectangle destination)
        {
            this.Render(texture, destination, new Rectangle(0, 0, texture.Width, texture.Height));
        }
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        public void Render(ITexture texture, Rectangle destination, Rectangle origin)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnRenderTexture(this, e), () => new RenderTextureEvent(texture, destination, origin)))
            {
                this.RenderManager.Render(texture, destination, origin);
            }
        }
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        public void Render(IFont font, string text, Vector2 position)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnRenderText(this, e), () => new RenderTextEvent(font, text, position)))
            {
                this.TextRasterizer.Render(font, text, position);
            }
        }
        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawRectangle(this, e), () => new DrawRectangleEvent(pen, rectangle)))
            {
                this.RenderManager.DrawRectangle(pen, rectangle);
            }
        }
        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public void DrawRectangle(IPen pen, Rectangle rectangle, float cornerRadius)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawRectangle(this, e), () => new DrawRectangleEvent(pen, rectangle, cornerRadius)))
            {
                this.RenderManager.DrawRectangle(pen, rectangle, cornerRadius);
            }
        }
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        public void DrawEllipse(IPen pen, Rectangle region)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawEllipse(this, e), () => new DrawEllipseEvent(pen, region)))
            {
                this.RenderManager.DrawEllipse(pen, region);
            }
        }
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        public void DrawPolygon(IPen pen, Vector2[] vertices)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawPolygon(this, e), () => new DrawPolygonEvent(pen, vertices)))
            {
                this.RenderManager.DrawPolygon(pen, vertices);
            }
        }
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void DrawLine(IPen pen, Vector2 start, Vector2 end)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawLine(this, e), () => new DrawLineEvent(pen, start, end)))
            {
                this.RenderManager.DrawLine(pen, start, end);
            }
        }
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawArc(this, e), () => new DrawArcEvent(pen, region, startAngle, sweepAngle)))
            {
                this.RenderManager.DrawArc(pen, region, startAngle, sweepAngle);
            }
        }
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnDrawPie(this, e), () => new DrawPieEvent(pen, region, startAngle, sweepAngle)))
            {
                this.RenderManager.DrawPie(pen, region, startAngle, sweepAngle);
            }
        }
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnFillRectangle(this, e), () => new FillRectangleEvent(brush, rectangle)))
            {
                this.RenderManager.FillRectangle(brush, rectangle);
            }
        }
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnFillRectangle(this, e), () => new FillRectangleEvent(brush, rectangle, cornerRadius)))
            {
                this.RenderManager.FillRectangle(brush, rectangle, cornerRadius);
            }
        }
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public void FillEllipse(IBrush brush, Rectangle region)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnFillElipse(this, e), () => new FillEllipseEvent(brush, region)))
            {
                this.RenderManager.FillEllipse(brush, region);
            }
        }
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        public void FillPolygon(IBrush brush, Vector2[] vertices)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnFillPolygon(this, e), () => new FillPolygonEvent(brush, vertices)))
            {
                this.RenderManager.FillPolygon(brush, vertices);
            }
        }
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            if (this.InterceptAndShouldProceed(
                (processor, e) => processor.OnFillPie(this, e), () => new FillPieEvent(brush, region, startAngle, sweepAngle)))
            {
                this.RenderManager.FillPie(brush, region, startAngle, sweepAngle);
            }
        }
        /// <summary>
        /// Presents all drawn data.
        /// </summary>
        public void Present()
        {
            foreach (IEffectProcessor processor in this._processors)
            {
                processor.OnPresent(this, this.BackBuffer);
            }

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
