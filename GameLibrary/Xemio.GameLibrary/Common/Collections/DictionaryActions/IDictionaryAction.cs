using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Collections.DictionaryActions
{
    internal interface IDictionaryAction<TKey, TValue>
    {
        /// <summary>
        /// Applies the action to the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        void Apply(Dictionary<TKey, TValue> dictionary);
    }
}
