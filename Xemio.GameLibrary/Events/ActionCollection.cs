using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    public class ActionCollection<T> : List<Action<T>>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCollection"/> class.
        /// </summary>
        public ActionCollection()
        {

        }
        #endregion
    }
}
