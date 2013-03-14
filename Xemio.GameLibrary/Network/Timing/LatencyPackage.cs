using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Network.Packages;

namespace Xemio.GameLibrary.Network.Timing
{
    public class LatencyPackage : Package
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LatencyPackage"/> class.
        /// </summary>
        public LatencyPackage()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the latency.
        /// </summary>
        public float Latency { get; set; }
        #endregion
    }
}
