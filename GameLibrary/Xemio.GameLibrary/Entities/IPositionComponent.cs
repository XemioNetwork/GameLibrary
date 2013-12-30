using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Entities
{
    public interface IPositionComponent
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Offset { get; }
    }
}
