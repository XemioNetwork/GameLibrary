using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.Loops
{
    internal class LoopController
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LoopController"/> class.
        /// </summary>
        /// <param name="sound">The sound.</param>
        public LoopController(ISound sound)
        {
            this.Sound = sound;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sound.
        /// </summary>
        public ISound Sound { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this.Sound.Duration > 0)
            {
                if (this.Sound.Position >= this.Sound.Duration)
                {
                    SoundManager soundManager = XGL.Components.Get<SoundManager>();
                    soundManager.Play(this.Sound, PlayMode.KeepLocation);
                }
            }
        }
        #endregion
    }
}
