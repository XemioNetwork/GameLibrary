using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Caching
{
    public class InstanceNotCachedException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceNotCachedException"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public InstanceNotCachedException(object instance)
            : base("The file name for instance '" + instance + "' does not exist inside the file cache. " +
                   "Please load or save it first using the content manager.")
        {
            this.Instance = instance;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public object Instance { get; private set; }
        #endregion
    }
}
