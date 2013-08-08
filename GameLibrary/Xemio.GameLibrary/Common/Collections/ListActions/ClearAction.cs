using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public class ClearAction<T> : IListAction<T>
    {
        #region Implementation of IListAction<T>
        /// <summary>
        /// Clears the list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Apply(List<T> list)
        {
            list.Clear();
        }
        #endregion
    }
}
