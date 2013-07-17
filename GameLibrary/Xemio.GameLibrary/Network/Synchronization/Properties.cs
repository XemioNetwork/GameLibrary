using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Synchronization
{
    public static class Properties
    {
        #region Properties
        /// <summary>
        /// Gets the comparator for changed properties.
        /// </summary>
        public static IPropertyComparator Changes
        {
            get { return new PropertyComparator(); }
        }
        /// <summary>
        /// Gets the comparator for all properties.
        /// </summary>
        public static IPropertyComparator All
        {
            get { return new StaticComparator(); }
        }
        /// <summary>
        /// Gets the comparator for none of the properties.
        /// </summary>
        public static IPropertyComparator None
        {
            get { return new StaticComparator { Value = false }; }
        }
        #endregion
    }
}
