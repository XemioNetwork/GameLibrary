using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.Collections
{
    public class DictionaryCatalog<T> : ICatalog<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryCatalog{T}"/> class.
        /// </summary>
        public DictionaryCatalog()
        {
            this.Items = new Dictionary<Type, T>();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the items.
        /// </summary>
        protected Dictionary<Type, T> Items { get; private set; } 
        #endregion

        #region Implementation of ICatalog<T>
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Install(T item)
        {
            this.Items.Add(item.GetType(), item);
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            this.Items.Remove(item.GetType());
        }
        /// <summary>
        /// Removes an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public void Remove<TItem>() where TItem : T
        {
            this.Items.Remove(typeof(TItem));
        }
        /// <summary>
        /// Gets an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Get<TItem>() where TItem : T
        {
            if (this.Items.ContainsKey(typeof(TItem)))
            {
                return (TItem)this.Items[typeof(TItem)];
            }

            return default(TItem);
        }
        /// <summary>
        /// Requires the specified item. Throws an exception, if it does not exist.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Require<TItem>() where TItem : T
        {
            var item = this.Get<TItem>();
            if (object.Equals(item, default(TItem)))
            {
                throw new InvalidOperationException("Required item of type " + typeof(TItem) + " not found.");
            }

            return item;
        }
        #endregion
    }
}
