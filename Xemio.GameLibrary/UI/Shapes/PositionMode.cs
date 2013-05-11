using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.UI.Shapes
{
    public enum PositionMode
    {
        /// <summary>
        /// Uses the specified bounds as multiplier for the widget bounds.
        /// </summary>
        Relative,
        /// <summary>
        /// Uses the specified bounds as pixel-representations.
        /// </summary>
        Absolute
    }
}
