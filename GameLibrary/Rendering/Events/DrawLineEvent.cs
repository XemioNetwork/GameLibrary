using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class DrawLineEvent : ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawLineEvent" /> class.
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public DrawLineEvent(IPen pen, Vector2 start, Vector2 end)
        {
            this.Pen = pen;
            this.Start = start;
            this.End = end;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the pen.
        /// </summary>
        public IPen Pen { get; set; }
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public Vector2 Start { get; set; }
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public Vector2 End { get; set; }
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
