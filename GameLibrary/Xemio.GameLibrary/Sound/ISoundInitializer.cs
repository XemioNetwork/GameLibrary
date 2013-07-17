using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Sound
{
    public interface ISoundInitializer : IComponent
    {
        /// <summary>
        /// Creates the provider.
        /// </summary>
        ISoundProvider CreateProvider();
    }
}
