﻿using SharpDX.Direct2D1;
using System;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;
using DXColor = SharpDX.Color;

namespace Xemio.GameLibrary.Rendering.SharpDX.Geometry
{
    internal class SharpDXGeometryFactory : IGeometryFactory
    {
        #region Implementation of IGeometryFactory
        /// <summary>
        /// Creates a new SharpDXPen
        /// </summary>
        /// <param name="color">Color of the pen</param>
        /// <param name="thickness">Thickness of the pen</param>
        /// <returns>new SharpDXPen</returns>
        public IPen CreatePen(Color color, float thickness)
        {
            return new SharpDXPen(color, thickness);
        }
        /// <summary>
        /// Creates a new SharpDXPen
        /// </summary>
        /// <param name="color">Color of the pen</param>
        /// <returns></returns>
        public IPen CreatePen(Color color)
        {
            return this.CreatePen(color, 1.0f);
        }
        /// <summary>
        /// Create a new gradient brush.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="angle">Not supported.</param>
        /// <returns></returns>
        public IBrush CreateGradient(Color top, Color bottom, int width, int height, float angle)
        {
            /// Brush properties
            LinearGradientBrushProperties properties = new LinearGradientBrushProperties()
            {
                StartPoint =
                    SharpDXHelper.CreateDrawingPoint(new Vector2(0, 0)),
                EndPoint =
                    SharpDXHelper.CreateDrawingPoint(new Vector2(width, height))
            };

            // Gradient stop
            GradientStop[] stops =
            {
                new GradientStop()
                {
                    Position = 0,
                    Color = SharpDXHelper.CreateColor(top)
                },
                new GradientStop()
                {
                    Position = 1,
                    Color = SharpDXHelper.CreateColor(bottom)
                }
            };

            GradientStopCollection stopCollection = new GradientStopCollection(SharpDXHelper.RenderTarget, stops);

            return new SharpDXBrush(
                new LinearGradientBrush(
                    SharpDXHelper.RenderTarget,
                    properties,
                    stopCollection),
                height, width);
        }
        /// <summary>
        /// Create a new solid brush
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public IBrush CreateSolid(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(
                SharpDXHelper.RenderTarget,
                new DXColor(color.R, color.G, color.B, color.A));

            return new SharpDXBrush(brush, 1, 1);
        }
        /// <summary>
        /// Create a new texture brush
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public IBrush CreateTexture(ITexture texture)
        {
            SharpDXTexture sdxTexture = texture as SharpDXTexture;
            if (sdxTexture == null)
            {
                throw new ArgumentException("Argument has to be a SharpDXTexture", "texture");
            }

            return new SharpDXBrush(new BitmapBrush(SharpDXHelper.RenderTarget, sdxTexture.Bitmap), 0, 0);
        }
        #endregion

    }
}
