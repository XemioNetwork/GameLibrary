using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.Collections
{
    public class LinearCatalog<T> : ICatalog<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearCatalog{T}"/> class.
        /// </summary>
        public LinearCatalog()
        {
            this._items = new Dictionary<Type, T>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Type, T> _items; 
        #endregion

        #region Implementation of ICatalog<T>
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Install(T item)
        {
            this._items.Add(item.GetType(), item);
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            this._items.Remove(item.GetType());
        }
        /// <summary>
        /// Removes an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public void Remove<TItem>() where TItem : class, T
        {
            this._items.Remove(typeof(TItem));
        }
        /// <summary>
        /// Gets an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Get<TItem>() where TItem : class, T
        {
            if (this._items.ContainsKey(typeof (TItem)))
            {
                return (TItem)this._items[typeof(TItem)];
            }

            return default(TItem);
        }
        /// <summary>
        /// Requires the specified item. Throws an exception, if it does not exist.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Require<TItem>() where TItem : class, T
        {
            var element = this.Get<TItem>();
            if (element == null)
            {
                throw new InvalidOperationException("Required element does not exist inside the catalog.");
            }

            return element;
        }
        #endregion
    }
}
