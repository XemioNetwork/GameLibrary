using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound
{
    public interface ISound
    {
        /// <summary>
        /// Gets or sets the sound radius.
        /// </summary>
        float Radius { get; set; }
        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        float Volume { get; set; }
        /// <summary>
        /// Gets or sets the speed ratio.
        /// </summary>
        float SpeedRatio { get; set; }
        /// <summary>
        /// Gets or sets the position in seconds.
        /// </summary>
        float Position { get; set; }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        float Balance { get; set; }
        /// <summary>
        /// Gets the duration in seconds.
        /// </summary>
        float Duration { get; }
    }
}
