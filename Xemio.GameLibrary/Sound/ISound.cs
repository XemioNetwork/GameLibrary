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
        /// Plays the sound.
        /// </summary>
        void Play();
        /// <summary>
        /// Plays the sound looped.
        /// </summary>
        void PlayLooping();
        /// <summary>
        /// Stops the current playing sound.
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        float Volume { get; set; }
        /// <summary>
        /// Gets or sets the speed ratio.
        /// </summary>
        float SpeedRatio { get; set; }
        /// <summary>
        /// Gets the duration.
        /// </summary>
        float Duration { get; }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        float Balance { get; set; }
    }
}
