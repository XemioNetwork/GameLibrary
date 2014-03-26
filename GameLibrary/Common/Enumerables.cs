using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class Enumerables
    {
        #region Methods
        /// <summary>
        /// Converts an enumerable structure to a dictionary by selecting 2 elements per one dictionary element.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified enumerables element count must be devidable by 2.</exception>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(IEnumerable enumerable)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            IEnumerator enumerator = enumerable.GetEnumerator();

            for (int i = 0; enumerator.Current != null; i++)
            {
                object key = enumerator.Current;
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("enumerable", @"The specified enumerables element count must be devidable by 2.");
                }

                object value = enumerator.Current;
                enumerator.MoveNext();

                dictionary.Add((TKey)key, (TValue)value);
            }

            return dictionary;
        }
        #endregion
    }
}
