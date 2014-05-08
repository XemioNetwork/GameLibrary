using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class VertexEvent : IEvent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the vertices.
        /// </summary>
        public Vector2[] Vertices { get; set; }
        #endregion
    }
}
