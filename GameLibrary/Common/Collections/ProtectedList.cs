﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Collections.ListActions;

namespace Xemio.GameLibrary.Common.Collections
{
    public class ProtectedList<T> : IList<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedList{T}"/> class.
        /// </summary>
        public ProtectedList()
        {
            this._list = new List<T>();
            this._actions = new List<IListAction<T>>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedList{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ProtectedList(int capacity)
        {
            this._list = new List<T>(capacity);
            this._actions = new List<IListAction<T>>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedList{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ProtectedList(IEnumerable<T> collection)
        {
            this._list = new List<T>(collection);
            this._actions = new List<IListAction<T>>();
        }
        #endregion

        #region Fields
        private readonly List<T> _list;
        private readonly List<IListAction<T>> _actions;
        private int _protectionCount = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is protected.
        /// </summary>
        public bool IsProtected
        {
            get { return this._protectionCount > 0; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the caching.
        /// </summary>
        public IDisposable Protect()
        {
            this._protectionCount++;

            return new ActionDisposable(() =>
            {
                this._protectionCount--;

                if (this.IsProtected == false)
                    this.ApplyChanges();
            });
        }
        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            foreach (IListAction<T> action in this._actions)
            {
                action.Apply(this._list);
            }

            this._actions.Clear();
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Implementation of ICollection<T>
        /// <summary>
        /// Adds the item to the list.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            if (this.IsProtected)
            { 
                this._actions.Add(new AddAction<T>(item));
                return;
            }

            lock (this._list) { this._list.Add(item); }
        }
        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            if (this.IsProtected)
            { 
                this._actions.Add(new ClearAction<T>());
                return;
            }

            lock (this._list) { this._list.Clear(); }
        }
        /// <summary>
        /// Determines wether the specified item is inside the list.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Contains(T item)
        {
            lock (this._list) { return this._list.Contains(item); }
        }
        /// <summary>
        /// Copies all elements to an array.
        /// </summary>
        /// <param name="array">The array</param>
        /// <param name="arrayIndex">The array index.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this._list) { this._list.CopyTo(array, arrayIndex); }
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Remove(T item)
        {
            if (this.IsProtected)
            { 
                this._actions.Add(new RemoveAction<T>(item));
                return true;
            }

            lock (this._list) { return this._list.Remove(item); }
        }
        /// <summary>
        /// Gets the item count.
        /// </summary>
        public int Count
        {
            get { return this._list.Count; }
        }
        /// <summary>
        /// Determines wether the list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Implementation of IList<T>
        /// <summary>
        /// Returns the index of the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public int IndexOf(T item)
        {
            return this._list.IndexOf(item);
        }
        /// <summary>
        /// Inserts the specified item into the list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        public void Insert(int index, T item)
        {
            if (this.IsProtected)
            { 
                this._actions.Add(new InsertAction<T>(index, item));
                return;
            }

            lock (this._list) { this._list.Insert(index, item); }
        }
        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            if (this.IsProtected)
            { 
                this._actions.Add(new RemoveAtAction<T>(index));
                return;
            }

            lock(this._list) { this._list.RemoveAt(index); }
        }
        /// <summary>
        /// Gets or sets an item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public T this[int index]
        {
            get { return this._list[index]; }
            set
            {
                if (this.IsProtected)
                { 
                    this._actions.Add(new IndexerAction<T>(index, value));
                    return;
                }

                lock (this._list) { this._list[index] = value; }
            }
        }
        #endregion
    }
}
