using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public class AddAction<T> : IListAction<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public AddAction(T value)
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
        /// Adds the item to the list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list.Add(this.Value);
        }
        #endregion
    }
}
