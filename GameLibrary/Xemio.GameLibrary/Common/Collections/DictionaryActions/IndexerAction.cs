using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Collections.DictionaryActions
{
    public class IndexerAction<TKey, TValue> : IDictionaryAction<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerAction&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public IndexerAction(TKey key, TValue value)
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
        
        #region Implementation of IDictionaryAction<TKey,TValue>
        /// <summary>
        /// Sets the item for the specified key.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public void Apply(Dictionary<TKey, TValue> dictionary)
        {
            dictionary[this.Key] = this.Value;
        }
        #endregion
    }
}
