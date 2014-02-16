using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.GameLibrary.Rendering.HTML5.Geometry
{
    public class HTMLGeometryFactory : IGeometryFactory
    {
        #region Constructors
        public HTMLGeometryFactory()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Implementation of IGeometryFactory

        public IPen CreatePen(Color color)
        {
            throw new NotImplementedException();
        }

        public IPen CreatePen(Color color, float thickness)
        {
            throw new NotImplementedException();
        }

        public IBrush CreateSolid(Color color)
        {
            throw new NotImplementedException();
        }

        public IBrush CreateGradient(Color top, Color bottom, Vector2 @from, Vector2 to)
        {
            throw new NotImplementedException();
        }

        public IBrush CreateTexture(ITexture texture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
