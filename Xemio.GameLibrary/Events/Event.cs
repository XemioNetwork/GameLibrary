using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Events
{
    public abstract class Event
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Event"/> is synced.
        /// </summary>
        public virtual bool Synced
        {
            get { return false; }
        }
        #endregion
    }
}
