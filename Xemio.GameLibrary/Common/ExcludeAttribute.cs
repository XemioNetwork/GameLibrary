using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public class ExcludeAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeAttribute"/> class.
        /// Used to exclude a property from serialization.
        /// </summary>
        public ExcludeAttribute()
        {

        }
        #endregion
    }
}
