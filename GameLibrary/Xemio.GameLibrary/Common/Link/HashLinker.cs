using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common.Link
{
    public class HashLinker<TKey, TValue> : GenericLinker<TKey, TValue> where TValue : ILinkable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public HashLinker() : base()
        {
            this.InitializeMappings();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public HashLinker(Assembly assembly) : base(assembly)
        {
            this.InitializeMappings();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public HashLinker(IEnumerable<string> assemblies) : base(assemblies)
        {
            this.InitializeMappings();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public HashLinker(IEnumerable<Assembly> assemblies) : base(assemblies)
        {
            this.InitializeMappings();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HashLinker&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public HashLinker(string assemblyName) : base(assemblyName)
        {
            this.InitializeMappings();
        }
        #endregion

        #region Fields
        private Dictionary<int, TValue> _hashMappings; 
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the mappings.
        /// </summary>
        private void InitializeMappings()
        {
            this._hashMappings = new Dictionary<int, TValue>();
        }
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void Add(TKey key, TValue value)
        {
            this._hashMappings.Add(key.GetHashCode(), value);
            base.Add(key, value);
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
