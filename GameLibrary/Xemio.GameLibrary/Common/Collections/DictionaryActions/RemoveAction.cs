using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Collections.DictionaryActions
{
    public class RemoveAction<TKey, TValue> : IDictionaryAction<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveAction&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public RemoveAction(TKey key)
        {
            this.Key = key;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the key.
        /// </summary>
        public TKey Key { get; private set; }
        #endregion
        
        #region Implementation of IDictionaryAction<TKey,TValue>
        /// <summary>
        /// Removes the item mapped to the specified key from the dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public void Apply(Dictionary<TKey, TValue> dictionary)
        {
            dictionary.Remove(this.Key);
        }
        #endregion
    }
}
