using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Network.Synchronization
{
    public class StaticComparator : IPropertyComparator
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticComparator"/> class.
        /// </summary>
        public StaticComparator()
        {
            this.Value = true;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StaticComparator"/>'s value.
        /// </summary>
        public bool Value { get; set; }
        #endregion

        #region IPropertyComparator Member
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> are equal to each other.
        /// </summary>
        /// <param name="a">The <see cref="System.Object"/> to compare with.</param>
        /// <param name="b">The <see cref="System.Object"/> to compare with.</param>
        /// <returns></returns>
        public bool IsEqual(object a, object b)
        {
            return this.Value;
        }
        #endregion
    }
}
