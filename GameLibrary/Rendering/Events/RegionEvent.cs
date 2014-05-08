using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.GameLibrary.Rendering.Events
{
    public class RegionEvent : IEvent
    {
        #region Properties
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        public Rectangle Region { get; set; }
        #endregion
    }
}
