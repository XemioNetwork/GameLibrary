using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Link
{
    public class HashLinker<TKey, TValue> : Linker<TKey, TValue> where TValue : ILinkable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public HashLinker()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public HashLinker(Assembly assembly) : base(assembly)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public HashLinker(IEnumerable<string> assemblies) : base(assemblies)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public HashLinker(IEnumerable<Assembly> assemblies) : base(assemblies)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public HashLinker(string assemblyName) : base(assemblyName)
        {
        }
        #endregion

        #region Fields
        private readonly Dictionary<int, TValue> _hashMappings = new Dictionary<int, TValue>(); 
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void Add(TValue value)
        {
            this._hashMappings.Add(value.Id.GetHashCode(), value);
            base.Add(value);
        }
        /// <summary>
        /// Resolves the specified hash code.
        /// </summary>
        /// <param name="hashCode">The hash code.</param>
        public TValue Resolve(int hashCode)
        {
            if (!this._hashMappings.ContainsKey(hashCode))
                return default(TValue);

            return this._hashMappings[hashCode];
        }
        #endregion
    }
}
