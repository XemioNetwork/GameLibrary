using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.Common
{
    public class SoundFactory : ISoundFactory
    {
        #region ISoundFactory Member
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ISound CreateSound(string fileName)
        {
            return new Sound(fileName);
        }
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public ISound CreateSound(Stream stream)
        {
            string temoraryFileName = Path.GetRandomFileName();
            using (FileStream fileStream = new FileStream(temoraryFileName, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }

            ISound sound = this.CreateSound(temoraryFileName);
            File.Delete(temoraryFileName);

            return sound;
        }
        /// <summary>
        /// Creates a sound.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ISound CreateSound(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                return this.CreateSound(stream);
            }
        }
        #endregion
    }
}
