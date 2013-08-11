using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Synchronization
{
    public class ChangedComparator : IPropertyComparator
    {
        #region IPropertyComparator Member
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> are equal to each other.
        /// </summary>
        /// <param name="a">The <see cref="System.Object"/> to compare with.</param>
        /// <param name="b">The <see cref="System.Object"/> to compare with.</param>
        public bool HasChanged(object a, object b)
        {
            return !object.Equals(a, b);
        }
        #endregion
    }
}
