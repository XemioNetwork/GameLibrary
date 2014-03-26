using System;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections
{
    public class AutoProtectedDictionary<TKey, TValue> : ProtectedDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region Overrides of ProtectedDictionary<T>
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            using (this.Protect())
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

