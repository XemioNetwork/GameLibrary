using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.WPF
{
    internal class WPFSoundProvider : ISoundProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WPFSoundProvider"/> class.
        /// </summary>
        public WPFSoundProvider()
        {
            this.Factory = new WPFSoundFactory();
        }
        #endregion

        #region ISoundProvider Member
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public ISoundFactory Factory { get; private set; }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public IDisposable Play(ISound sound)
        {
            WPFSound internalSound = sound as WPFSound;
            if (internalSound == null)
            {
                throw new InvalidOperationException("You have to use the according sound factory to create a sound.");
            }

            internalSound.MediaPlayer.Stop();
            internalSound.MediaPlayer.Play();

            return new SoundDisposer(internalSound);
        }
        #endregion
    }
}
