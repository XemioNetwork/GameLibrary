using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    internal class RemoveAction<T> : IListAction<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RemoveAction(T value)
        {
            this.Value = value;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }
        #endregion
        
        #region Implementation of IListAction<T>
        /// <summary>
        /// Removes the specified item from the list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list.Remove(this.Value);
        }
        #endregion
    }
}
