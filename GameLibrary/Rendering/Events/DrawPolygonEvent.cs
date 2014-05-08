using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class DrawPolygonEvent : VertexEvent, ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawPolygonEvent" /> class.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="vertices">The vertices.</param>
        public DrawPolygonEvent(IPen pen, Vector2[] vertices)
        {
            this.Pen = pen;
            this.Vertices = vertices;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the pen.
        /// </summary>
        public IPen Pen { get; set; }
        #endregion

        #region Implementation of ICancelableEvent
        /// <summary>
        /// Gets a value indicating whether the event propagation was canceled.
        /// </summary>
        public bool IsCanceled { get; private set; }
        /// <summary>
        /// Cancels the event propagation.
        /// </summary>
        public void Cancel()
        {
            this.IsCanceled = true;
        }
        #endregion
    }
}
