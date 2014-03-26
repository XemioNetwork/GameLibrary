using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Collections
{
    public class AutoProtectedList<T> : ProtectedList<T>
    {
        #region Overrides of ProtectedList<T>
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public override IEnumerator<T> GetEnumerator()
        {
            using (this.Protect())
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
