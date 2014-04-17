using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Network.Timing
{
    public class LatencyInformation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LatencyInformation"/> class.
        /// </summary>
        /// <param name="latency">The latency.</param>
        public LatencyInformation(float latency)
        {
            this.Latency = latency;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the latency.
        /// </summary>
        public float Latency { get; private set; }
        #endregion
    }
}
