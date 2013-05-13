using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Plugins
{
    public interface ILibraryInitializer
    {
        /// <summary>
        /// Initializes this library.
        /// </summary>
        void Initialize();
    }
}
