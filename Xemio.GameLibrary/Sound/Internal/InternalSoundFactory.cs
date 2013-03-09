using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.Internal
{
    internal class InternalSoundFactory : ISoundFactory
    {
        #region ISoundFactory Member
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ISound CreateSound(string fileName)
        {
            ISound sound = new InternalSound(fileName);
            sound.Radius = float.MaxValue;

            return sound;
        }
        #endregion
    }
}
