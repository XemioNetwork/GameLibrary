using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math
{
    public interface IIntersectable<T>
    {
        /// <summary>
        /// Determines wether the two objects are intersecting.
        /// </summary>
        /// <param name="value">The value.</param>
        bool Intersects(T value);
        /// <summary>
        /// Calculates the minimum translation vector for the specified intersection.
        /// </summary>
        /// <param name="value">The value.</param>
        Vector2 Intersect(T value);
    }
}
