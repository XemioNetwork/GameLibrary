using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Common
{
    public static class Disposable
    {
        #region Static Methods
        /// <summary>
        /// Combines the specified disposables.
        /// </summary>
        /// <param name="disposables">The disposables.</param>
        public static IDisposable Combine(params IDisposable[] disposables)
        {
            return new ActionDisposable(() =>
            {
                foreach (IDisposable disposable in disposables)
                {
                    disposable.Dispose();
                }
            });
        }
        #endregion
    }
}
