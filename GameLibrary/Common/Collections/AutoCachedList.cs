using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Collections
{
    public class AutoCachedList<T> : CachedList<T>
    {
        #region Overrides of CachedList<T>
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public override IEnumerator<T> GetEnumerator()
        {
            using (this.StartCaching())
            {
                IEnumerator<T> enumerator = base.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
        #endregion
    }
}
