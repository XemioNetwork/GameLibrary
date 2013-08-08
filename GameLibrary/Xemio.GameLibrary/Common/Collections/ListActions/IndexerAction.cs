using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public class IndexerAction<T> : IListAction<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public IndexerAction(int index, T value)
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
        /// Sets the value at the specified index.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list[this.Index] = this.Value;
        }
        #endregion
    }
}
