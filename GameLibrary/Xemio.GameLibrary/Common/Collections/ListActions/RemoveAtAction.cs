using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public class RemoveAtAction<T> : IListAction<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveAtAction&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        public RemoveAtAction(int index)
        {
            this.Index = index;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the index.
        /// </summary>
        public int Index { get; private set; }
        #endregion
        
        #region Implementation of IListAction<T>
        /// <summary>
        /// Applies action to the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list.RemoveAt(this.Index);
        }
        #endregion
    }
}
