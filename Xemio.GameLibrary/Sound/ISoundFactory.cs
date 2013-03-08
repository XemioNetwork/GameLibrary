using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound
{
    public interface ISoundFactory
    {
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        ISound CreateSound(string fileName);
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="stream">The stream.</param>
        ISound CreateSound(Stream stream);
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="data">The data.</param>
        ISound CreateSound(byte[] data);
    }
}
