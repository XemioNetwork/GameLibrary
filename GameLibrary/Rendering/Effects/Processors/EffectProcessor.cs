using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Events;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    [ManuallyLinked]
    public abstract class EffectProcessor<T> : IEffectProcessor where T : IEffect
    {
        #region Protected Methods
        /// <summary>
        /// Finds the specified processor.
        /// </summary>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        protected TProcessor Find<TProcessor>() where TProcessor : IEffectProcessor
        {
            var implementations = XGL.Components.Require<IImplementationManager>();
            var processor = implementations.Get<Type, IEffectProcessor>(typeof(TProcessor));

            return (TProcessor)processor;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected abstract void Enable(T effect, GraphicsDevice graphicsDevice);
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        protected abstract void Disable(T effect, GraphicsDevice graphicsDevice);
        /// <summary>
        /// Called when vertices are getting rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnVertices(GraphicsDevice graphicsDevice, VertexEvent evt)
        {
        }
        /// <summary>
        /// Called when a region gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnRegion(GraphicsDevice graphicsDevice, RegionEvent evt)
        {
        }
        /// <summary>
        /// Called when a texture gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnRenderTexture(GraphicsDevice graphicsDevice, RenderTextureEvent evt)
        {
        }
        /// <summary>
        /// Called when a text gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnRenderText(GraphicsDevice graphicsDevice, RenderTextEvent evt)
        {
        }
        /// <summary>
        /// Called when a rectangle gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawRectangle(GraphicsDevice graphicsDevice, DrawRectangleEvent evt)
        {
        }
        /// <summary>
        /// Called when an ellipse gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawEllipse(GraphicsDevice graphicsDevice, DrawEllipseEvent evt)
        {
        }
        /// <summary>
        /// Called when a polygon gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawPolygon(GraphicsDevice graphicsDevice, DrawPolygonEvent evt)
        {
        }
        /// <summary>
        /// Called when a line gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawLine(GraphicsDevice graphicsDevice, DrawLineEvent evt)
        {
        }
        /// <summary>
        /// Called when an arc gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawArc(GraphicsDevice graphicsDevice, DrawArcEvent evt)
        {
        }
        /// <summary>
        /// Called when a pie gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnDrawPie(GraphicsDevice graphicsDevice, DrawPieEvent evt)
        {
        }
        /// <summary>
        /// Called when a rectangle gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnFillRectangle(GraphicsDevice graphicsDevice, FillRectangleEvent evt)
        {
        }
        /// <summary>
        /// Called when an ellipse gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnFillElipse(GraphicsDevice graphicsDevice, FillEllipseEvent evt)
        {
        }
        /// <summary>
        /// Called when a polygon gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnFillPolygon(GraphicsDevice graphicsDevice, FillPolygonEvent evt)
        {
        }
        /// <summary>
        /// Called when a pie gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        public virtual void OnFillPie(GraphicsDevice graphicsDevice, FillPieEvent evt)
        {
        }
        /// <summary>
        /// Called when the final backbuffers gets presented and copied to the screen.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="backBuffer">The back buffer.</param>
        public virtual void OnPresent(GraphicsDevice graphicsDevice, IRenderTarget backBuffer)
        {
        }
        #endregion

        #region Implementation of IEffectProcessor
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        void IEffectProcessor.Enable(IEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Enable((T)effect, graphicsDevice);
        }
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        void IEffectProcessor.Disable(IEffect effect, GraphicsDevice graphicsDevice)
        {
            this.Disable((T)effect, graphicsDevice);
        }
        #endregion

        #region Implementation of ILinkable<Type>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public Type Id
        {
            get { return typeof(T); }
        }
        #endregion
    }
}
