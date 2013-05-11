using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Sound.WPF
{
    public class WPFSoundInitializer : ISoundInitializer
    {
        #region ISoundInitializer Member
        /// <summary>
        /// Creates the provider.
        /// </summary>
        public ISoundProvider CreateProvider()
        {
            return new WPFSoundProvider();
        }
        #endregion
    }
}
