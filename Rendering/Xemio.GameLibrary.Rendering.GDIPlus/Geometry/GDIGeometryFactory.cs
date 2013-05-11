﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.GDIPlus.Geometry
{
    using Rectangle = System.Drawing.Rectangle;

    public class GDIGeometryFactory : IGeometryFactory
    {
        #region Constructors
        public GDIGeometryFactory()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region IGeometryFactory Member
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public IPen CreatePen(Color color)
        {
            return new GDIPen(color, 1);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <returns></returns>
        public IPen CreatePen(Color color, float thickness)
        {
            return new GDIPen(color, thickness);
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public IBrush CreateSolid(Color color)
        {
            return new GDIBrush(new SolidBrush(GDIHelper.Convert(color)));
        }
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns></returns>
        public IBrush CreateGradient(Color top, Color bottom, int width, int height, float angle)
        {
            return new GDIBrush(
                new LinearGradientBrush(
                    new Rectangle(0, 0, width, height),
                    GDIHelper.Convert(top),
                    GDIHelper.Convert(bottom),
                    MathHelper.ToDegrees(angle)));
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns></returns>
        public IBrush CreateTexture(ITexture texture)
        {
            GDITexture gdiTexture = texture as GDITexture;
            if (gdiTexture == null)
            {
                throw new InvalidOperationException(
                    "You cannot create a texture brush using an unsupported ITexture implementation");
            }

            return new GDIBrush(new TextureBrush(gdiTexture.Bitmap));
        }
        #endregion
    }
}