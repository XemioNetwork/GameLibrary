using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Events;

namespace Xemio.GameLibrary.Rendering
{
    public class ResolutionChangedEvent : IEvent
    {
        #region Properties
        /// <summary>
        /// Gets the new display mode.
        /// </summary>
        public DisplayMode DisplayMode { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionChangedEvent"/> class.
        /// </summary>
        /// <param name="displayMode">The display mode.</param>
        public ResolutionChangedEvent(DisplayMode displayMode)
        {
            this.DisplayMode = displayMode;
        }
        #endregion

    }
}
