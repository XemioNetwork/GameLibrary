using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Script
{
    public interface IScript
    {
        /// <summary>
        /// Executes this script.
        /// </summary>
        IEnumerable Execute();
    }
}
