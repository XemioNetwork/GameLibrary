using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Network.Timing
{
    public class TimeSyncInformation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSyncInformation"/> class.
        /// </summary>
        public TimeSyncInformation()
        {
            this.Watch = Stopwatch.StartNew();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the watch.
        /// </summary>
        public Stopwatch Watch { get; private set; }
        /// <summary>
        /// Gets the roundstrip time.
        /// </summary>
        public float RoundstripTime
        {
            get { return this.Watch.ElapsedMilliseconds * 0.5f; }
        }
        #endregion
    }
}
