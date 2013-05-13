using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public interface ICondition
    {
        /// <summary>
        /// Determines whether this condition is valid.
        /// </summary>
        bool IsValid();
    }
}
