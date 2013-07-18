using System;
using System.IO;
using Xemio.GameLibrary.Sound;

namespace Xemio.GameLibrary.Content.Serialization
{
    public class SoundReader : ContentReader<ISound>
    {
        #region ContentReader<ISound> Member
        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public override object Read(string fileName)
        {
            SoundManager soundManager = XGL.Components.Get<SoundManager>();
            ISoundFactory factory = soundManager.Provider.Factory;

            return factory.CreateSound(fileName);
        }
        /// <summary>
        /// Reads a sound instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override ISound Read(BinaryReader reader)
        {
            throw new NotSupportedException("The sound system doesn't support streams.");
        }
        #endregion
    }
}
