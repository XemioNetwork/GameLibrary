using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public class InsertAction<T> : IListAction<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public InsertAction(int index, T value)
        {
            this.Index = index;
            this.Value = value;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }
        #endregion
        
        #region Implementation of IListAction<T>
        /// <summary>
        /// Inserts the specified item into the list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list.Insert(this.Index, this.Value);
        }
        #endregion
    }
}
