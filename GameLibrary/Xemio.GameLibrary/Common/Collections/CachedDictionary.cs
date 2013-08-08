﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Collections.DictionaryActions;

namespace Xemio.GameLibrary.Common.Collections
{
    public class CachedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CachedDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public CachedDictionary()
        {
            this._dictionary = new Dictionary<TKey, TValue>();
            this._actions = new List<IDictionaryAction<TKey, TValue>>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<TKey, TValue> _dictionary;
        private readonly List<IDictionaryAction<TKey, TValue>> _actions;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether to automatically apply changes.
        /// </summary>
        public bool AutoApplyChanges { get; set; }
        /// <summary>
        /// Gets the enumerable.
        /// </summary>
        protected IEnumerable<KeyValuePair<TKey, TValue>> Enumerable
        {
            get
            {
                foreach (KeyValuePair<TKey, TValue> pair in this._dictionary)
                    yield return pair;

                if (this.AutoApplyChanges)
                    this.ApplyChanges();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            foreach (IDictionaryAction<TKey, TValue> action in this._actions)
            {
                action.Apply(this._dictionary);
            }
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.Enumerable.GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._actions.Add(new ClearAction<TKey, TValue>());
        }
        /// <summary>
        /// Determines whether the dictionary contains the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.ContainsKey(item.Key) && object.Equals(this[item.Key], item.Value);
        }
        /// <summary>
        /// Copies the dictionary data to the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                array[arrayIndex++] = pair;
            }
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this.Contains(item))
            {
                return this.Remove(item.Key);
            }

            return false;
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this._dictionary.Count; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Implementation of IDictionary<TKey,TValue>
        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool ContainsKey(TKey key)
        {
            return this._dictionary.ContainsKey(key);
        }
        /// <summary>
        /// Mapps the specified value to the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            this._actions.Add(new AddAction<TKey, TValue>(key, value));
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public bool Remove(TKey key)
        {
            this._actions.Add(new RemoveAction<TKey, TValue>(key));
            return true;
        }
        /// <summary>
        /// Tries to get a value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return this._dictionary.TryGetValue(key, out value);
        }
        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        public TValue this[TKey key]
        {
            get { return this._dictionary[key]; }
            set { this._actions.Add(new IndexerAction<TKey, TValue>(key, value)); }
        }
        /// <summary>
        /// Gets the keys.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return this._dictionary.Keys; }
        }
        /// <summary>
        /// Gets the values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return this._dictionary.Values; }
        }
        #endregion
    }
}
