using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;

namespace Xemio.GameLibrary.Rendering.Geometry
{
    public class GeometryFactory : IGeometryFactory
    {
        #region Singleton
        /// <summary>
        /// Gets the empty geometry factory.
        /// </summary>
        public static IGeometryFactory Empty
        {
            get { return Singleton<GeometryFactory>.Value; }
        }
        #endregion

        #region IGeometryFactory Member
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public virtual IPen CreatePen(Color color)
        {
            return null;
        }
        /// <summary>
        /// Creates a pen.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        /// <returns></returns>
        public virtual IPen CreatePen(Color color, float thickness)
        {
            return null;
        }
        /// <summary>
        /// Creates a solid brush.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public virtual IBrush CreateSolid(Color color)
        {
            return null;
        }
        /// <summary>
        /// Creates a gradient brush.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public virtual IBrush CreateGradient(Color top, Color bottom, int width, int height, float angle)
        {
            return null;
        }
        /// <summary>
        /// Creates a texture brush.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns></returns>
        public virtual IBrush CreateTexture(ITexture texture)
        {
            return null;
        }
        #endregion
    }
}
