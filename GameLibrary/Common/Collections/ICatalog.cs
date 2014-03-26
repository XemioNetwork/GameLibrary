using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common.Collections
{
    public interface ICatalog<in T>
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Install(T item);
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);
        /// <summary>
        /// Removes an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        void Remove<TItem>() where TItem : class, T;
        /// <summary>
        /// Gets an item by a specified type.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        TItem Get<TItem>() where TItem : class, T;
        /// <summary>
        /// Requires the specified item. Throws an exception, if it does not exist.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        TItem Require<TItem>() where TItem : class, T;
    }
}
