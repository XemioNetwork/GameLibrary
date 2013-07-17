using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound
{
    public interface ISoundProvider
    {
        /// <summary>
        /// Gets the factory.
        /// </summary>
        ISoundFactory Factory { get; }
        /// <summary>
        /// Plays the specified sound.
        /// </summary>
        /// <param name="sound">The sound.</param>
        IDisposable Play(ISound sound);
    }
}
