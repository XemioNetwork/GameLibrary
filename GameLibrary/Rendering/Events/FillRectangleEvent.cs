using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class FillRectangleEvent : RegionEvent, ICancelableEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FillRectangleEvent"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        public FillRectangleEvent(IBrush brush, Rectangle region) : this(brush, region, 0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FillRectangleEvent" /> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        public FillRectangleEvent(IBrush brush, Rectangle region, float cornerRadius)
        {
            this.Brush = brush;
            this.Region = region;
            this.CornerRadius = cornerRadius;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        public IBrush Brush { get; set; }
        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        public float CornerRadius { get; set; }
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
