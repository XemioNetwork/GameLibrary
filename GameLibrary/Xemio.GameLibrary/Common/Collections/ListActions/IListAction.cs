using System.Collections.Generic;

namespace Xemio.GameLibrary.Common.Collections.ListActions
{
    public interface IListAction<T>
    {
        /// <summary>
        /// Applies action to the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        void Apply(List<T> list);
    }
}
