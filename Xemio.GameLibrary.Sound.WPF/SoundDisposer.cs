using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.WPF
{
    internal class SoundDisposer : IDisposable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundDisposer"/> class.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public SoundDisposer(WPFSound sound)
        {
            this.Sound = sound;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sound.
        /// </summary>
        public WPFSound Sound { get; private set; }
        #endregion

        #region IDisposable Member
        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.Sound.MediaPlayer.Stop();
        }
        #endregion
    }
}
