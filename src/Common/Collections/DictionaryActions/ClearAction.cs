using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Collections.DictionaryActions
{
    internal class ClearAction<TKey, TValue> : IDictionaryAction<TKey, TValue>
    {
        #region Implementation of IDictionaryAction<TKey,TValue>
        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public void Apply(Dictionary<TKey, TValue> dictionary)
        {
            dictionary.Clear();
        }
        #endregion
    }
}
