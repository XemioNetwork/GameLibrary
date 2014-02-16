using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Plugins;

namespace Xemio.GameLibrary.Network.Syncput
{
    public class SyncputInitializer : ILibraryInitializer
    {
        #region Implementation of ILibraryInitializer
        /// <summary>
        /// Initializes syncput.
        /// </summary>
        public void Initialize()
        {
            XGL.Components.Add(new Syncput());
        }
        #endregion
    }
}
