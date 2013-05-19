using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Mono.Geometry
{
    using Rectangle = System.Drawing.Rectangle;

    public class MonoGeometryFactory : IGeometryFactory
    {
        #region Constructors
        public MonoGeometryFactory()
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
            return new MonoPen(color, 1);
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <returns></returns>
        public IPen CreatePen(Color color, float thickness)
        {
            return new MonoPen(color, thickness);
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public IBrush CreateSolid(Color color)
        {
            return new MonoBrush(new SolidBrush(MonoHelper.Convert(color)));
        }
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="from">First point.</param>
        /// <param name="to">Second point.</param>
        /// <returns></returns>
        public IBrush CreateGradient(Color top, Color bottom, Vector2 from, Vector2 to)
        {
            return new MonoBrush(
                new LinearGradientBrush(
                    new PointF(from.X, from.Y),
                    new PointF(to.X, to.Y),
                    MonoHelper.Convert(top),
                    MonoHelper.Convert(bottom)));
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns></returns>
        public IBrush CreateTexture(ITexture texture)
        {
            MonoTexture MonoTexture = texture as MonoTexture;
            if (MonoTexture == null)
            {
                throw new InvalidOperationException(
                    "You cannot create a texture brush using an unsupported ITexture implementation");
            }

            return new MonoBrush(new TextureBrush(MonoTexture.Bitmap));
        }
        #endregion
    }
}
