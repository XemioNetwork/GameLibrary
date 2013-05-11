using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.WPF
{
    internal class WPFSoundFactory : ISoundFactory
    {
        #region ISoundFactory Member
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ISound CreateSound(string fileName)
        {
            ISound sound = new WPFSound(fileName);
            sound.Radius = float.MaxValue;

            return sound;
        }
        #endregion
    }
}
