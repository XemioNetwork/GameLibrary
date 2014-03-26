using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Config
{
    public interface IConfigurationAccess
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        Configuration Configuration { get; }
    }
}
