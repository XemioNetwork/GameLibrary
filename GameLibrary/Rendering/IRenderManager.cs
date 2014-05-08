﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Components.Attributes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering.Effects;
using Xemio.GameLibrary.Rendering.Effects.Processors;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.GameLibrary.Rendering
{
    public interface IRenderManager
    {
        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="color">The color.</param>
        void Clear(Color color);
        /// <summary>
        /// Renders the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="origin">The origin.</param>
        void Render(ITexture texture, Rectangle destination, Rectangle origin);
        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle);
        /// <summary>
        /// Draws a rounded rectangle.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        void DrawRectangle(IPen pen, Rectangle rectangle, float cornerRadius);
        /// <summary>
        /// Draws an ellipse.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        void DrawEllipse(IPen pen, Rectangle region);
        /// <summary>
        /// Draws a polygon.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        void DrawPolygon(IPen pen, Vector2[] vertices);
        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        void DrawLine(IPen pen, Vector2 start, Vector2 end);
        /// <summary>
        /// Draws an arc.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void DrawArc(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Draws a pie.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void DrawPie(IPen pen, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Fills the specified rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        void FillRectangle(IBrush brush, Rectangle rectangle);
        /// <summary>
        /// Fills a rounded rectangle.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        void FillRectangle(IBrush brush, Rectangle rectangle, float cornerRadius);
        /// <summary>
        /// Fills an ellipse.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        void FillEllipse(IBrush brush, Rectangle region);
        /// <summary>
        /// Fills a polygon.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="vertices">The vertices.</param>
        void FillPolygon(IBrush brush, Vector2[] vertices);
        /// <summary>
        /// Fills a pie.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        void FillPie(IBrush brush, Rectangle region, float startAngle, float sweepAngle);
        /// <summary>
        /// Presents this instance.
        /// </summary>
        void Present();
    }

    public static class RenderManagerExtensions
    {
        #region Effect Methods
        /// <summary>
        /// Translates the specified render manager.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="offset">The offset.</param>
        public static IDisposable Translate(this GraphicsDevice graphicsDevice, Vector2 offset)
        {
            return graphicsDevice.Apply(new TranslateEffect(offset));
        }
        /// <summary>
        /// Translates the specified render manager.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public static IDisposable Translate(this GraphicsDevice graphicsDevice, float x, float y)
        {
            return graphicsDevice.Apply(new TranslateEffect(new Vector2(x, y)));
        }
        /// <summary>
        /// Translates the render manager to the specified position.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="position">The position.</param>
        public static IDisposable TranslateTo(this GraphicsDevice graphicsDevice, Vector2 position)
        {
            return graphicsDevice.Apply(new TranslateToEffect(position));
        }
        /// <summary>
        /// Translates the render manager to the specified position.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public static IDisposable TranslateTo(this GraphicsDevice graphicsDevice, float x, float y)
        {
            return graphicsDevice.Apply(new TranslateToEffect(new Vector2(x, y)));
        }
        /// <summary>
        /// Tints the specified render manager using an alpha channel value.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="alpha">The alpha.</param>
        public static IDisposable Alpha(this GraphicsDevice graphicsDevice, float alpha)
        {
            return graphicsDevice.Apply(new TintEffect(new Color(1.0f, 1.0f, 1.0f, alpha)));
        }
        /// <summary>
        /// Tints the specified render manager.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="color">The color.</param>
        public static IDisposable Tint(this GraphicsDevice graphicsDevice, Color color)
        {
            return graphicsDevice.Apply(new TintEffect(color));
        }
        /// <summary>
        /// Tints the specified render manager.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="a">The alpha channel.</param>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        public static IDisposable Tint(this GraphicsDevice renderManager, float r, float g, float b, float a)
        {
            return renderManager.Apply(new TintEffect(new Color(r, g, b, a)));
        }
        /// <summary>
        /// Tints the specified render manager.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="color">The color.</param>
        /// <param name="blendMode">The blend mode.</param>
        public static IDisposable Tint(this GraphicsDevice renderManager, Color color, BlendMode blendMode)
        {
            return renderManager.Apply(new TintEffect(color, blendMode));
        }
        /// <summary>
        /// Rotates the specified render manager.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="rotation">The rotation.</param>
        public static IDisposable Rotate(this GraphicsDevice renderManager, float rotation)
        {
            return renderManager.Apply(new RotateEffect(rotation));
        }
        /// <summary>
        /// Flips the specified render manager.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="options">The options.</param>
        public static IDisposable Flip(this GraphicsDevice renderManager, FlipEffectOptions options)
        {
            return renderManager.Apply(new FlipEffect(options));
        }
        #endregion
    }
}
