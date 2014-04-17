using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.References
{
    public class StaticReference<T> : IReference<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticReference{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StaticReference(T value)
        {
            this.Value = value;
        }  
        #endregion

        #region Implementation of IReference<T>
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }
        #endregion
    }
}
