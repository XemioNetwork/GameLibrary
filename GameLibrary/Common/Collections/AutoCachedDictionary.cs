using System;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections
{
    public class AutoCachedDictionary<TKey, TValue> : CachedDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region Overrides of CachedDictionary<T>
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            using (this.StartCaching())
            {
                IEnumerator<KeyValuePair<TKey, TValue>> enumerator = base.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
        #endregion
    }
}

