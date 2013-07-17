using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Link
{
    /// <summary>
    /// A generic linker, that automatically loads all objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutomaticLinker<T> : GenericLinker<int, T> where T : ILinkable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticLinker&lt;T&gt;"/> class.
        /// </summary>
        public AutomaticLinker()
        {
            this.LoadFromAssemblyOf<T>();
        }
        #endregion
    }
    /// <summary>
    /// A generic linker, that automatically loads all objects.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class AutomaticLinker<TKey, TValue> : GenericLinker<TKey, TValue> where TValue : ILinkable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public AutomaticLinker()
        {
            this.LoadFromAssemblyOf<TValue>();
        }
        #endregion
    }
}
