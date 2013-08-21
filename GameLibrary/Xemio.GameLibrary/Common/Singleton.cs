using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public static class Singleton<T> where T : class, new()
    {
        #region Fields
        private static readonly T _instance = new T();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the static singleton instance.
        /// </summary>
        public static T Value
        {
            get { return _instance; }
        }
        #endregion
    }
}
