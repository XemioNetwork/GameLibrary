using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.Internal
{
    internal class InternalSoundProvider : ISoundProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalSoundProvider"/> class.
        /// </summary>
        public InternalSoundProvider()
        {
            this.Factory = new InternalSoundFactory();
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
            InternalSound internalSound = sound as InternalSound;
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
