using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class FillPieEvent : RegionEvent, ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FillPieEvent" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public FillPieEvent(IBrush brush, Rectangle region, float startAngle, float sweepAngle)
        {
            this.Brush = brush;
            this.Region = region;
            this.StartAngle = startAngle;
            this.SweepAngle = sweepAngle;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        public IBrush Brush { get; set; }
        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        public float StartAngle { get; set; }
        /// <summary>
        /// Gets or sets the sweep angle.
        /// </summary>
        public float SweepAngle { get; set; }
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
