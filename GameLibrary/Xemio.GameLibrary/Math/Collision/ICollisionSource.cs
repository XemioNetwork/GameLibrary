using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Math.Collision
{
    public interface ICollisionSource
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Position { get; }
    }
}
