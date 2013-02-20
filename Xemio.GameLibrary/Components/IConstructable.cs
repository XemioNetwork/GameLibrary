using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Components
{
    public interface IConstructable : IComponent
    {
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        void Construct();
    }
}
