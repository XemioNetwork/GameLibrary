using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.Collections
{
    public class ListCatalog<T> : ICatalog<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCatalog{T}"/> class.
        /// </summary>
        public ListCatalog()
        {
            this.Items = new List<T>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the items.
        /// </summary>
        protected List<T> Items { get; private set; } 
        #endregion

        #region Implementation of ICatalog<in T>
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Install(T item)
        {
            this.Items.Add(item);
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            this.Items.Remove(item);
        }
        /// <summary>
        /// Removes an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public void Remove<TItem>() where TItem : T
        {
            this.Remove(this.Get<TItem>());
        }
        /// <summary>
        /// Gets an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        public TItem Get<TItem>() where TItem : T
        {
            return (TItem)this.Items.FirstOrDefault(item => item is TItem);
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
