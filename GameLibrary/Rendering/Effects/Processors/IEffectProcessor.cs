using System;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Rendering.Events;

namespace Xemio.GameLibrary.Rendering.Effects.Processors
{
    [ManuallyLinked]
    public interface IEffectProcessor : ILinkable<Type>
    {
        /// <summary>
        /// Enables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        void Enable(IEffect effect, GraphicsDevice graphicsDevice);
        /// <summary>
        /// Disables the specified effect.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        void Disable(IEffect effect, GraphicsDevice graphicsDevice);
        /// <summary>
        /// Called when a region gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnRegion(GraphicsDevice graphicsDevice, RegionEvent evt);
        /// <summary>
        /// Called when vertices are getting rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnVertices(GraphicsDevice graphicsDevice, VertexEvent evt);
        /// <summary>
        /// Called when a texture gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnRenderTexture(GraphicsDevice graphicsDevice, RenderTextureEvent evt);
        /// <summary>
        /// Called when a text gets rendered.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnRenderText(GraphicsDevice graphicsDevice, RenderTextEvent evt);
        /// <summary>
        /// Called when a rectangle gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawRectangle(GraphicsDevice graphicsDevice, DrawRectangleEvent evt);
        /// <summary>
        /// Called when an ellipse gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawEllipse(GraphicsDevice graphicsDevice, DrawEllipseEvent evt);
        /// <summary>
        /// Called when a polygon gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawPolygon(GraphicsDevice graphicsDevice, DrawPolygonEvent evt);
        /// <summary>
        /// Called when a line gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawLine(GraphicsDevice graphicsDevice, DrawLineEvent evt);
        /// <summary>
        /// Called when an arc gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawArc(GraphicsDevice graphicsDevice, DrawArcEvent evt);
        /// <summary>
        /// Called when a pie gets drawn.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnDrawPie(GraphicsDevice graphicsDevice, DrawPieEvent evt);
        /// <summary>
        /// Called when a rectangle gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnFillRectangle(GraphicsDevice graphicsDevice, FillRectangleEvent evt);
        /// <summary>
        /// Called when an ellipse gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnFillElipse(GraphicsDevice graphicsDevice, FillEllipseEvent evt);
        /// <summary>
        /// Called when a polygon gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnFillPolygon(GraphicsDevice graphicsDevice, FillPolygonEvent evt);
        /// <summary>
        /// Called when a pie gets filled.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="evt">The evt.</param>
        void OnFillPie(GraphicsDevice graphicsDevice, FillPieEvent evt);
        /// <summary>
        /// Called when the final backbuffers gets presented and copied to the screen.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="backBuffer">The back buffer.</param>
        void OnPresent(GraphicsDevice graphicsDevice, IRenderTarget backBuffer);
    }
}
