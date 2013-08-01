using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    /// <summary>
    /// A pair used to provide dictionary-safe hashcode mappings.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Pair<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public Pair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the key.
        /// </summary>
        public TKey Key { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return (this.Key.GetHashCode() ^ this.Value.GetHashCode()) & 17;
        }
        #endregion
    }
}
