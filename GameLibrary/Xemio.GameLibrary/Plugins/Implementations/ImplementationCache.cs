using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Plugins.Implementations
{
    public class ImplementationCache
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationCache"/> class.
        /// </summary>
        public ImplementationCache()
        {
            this._cachedContexts = new Dictionary<IAssemblyContext, bool>();
            this._linkers = new Dictionary<Type, dynamic>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<IAssemblyContext, bool> _cachedContexts;
        private readonly Dictionary<Type, dynamic> _linkers;
        #endregion

        #region Methods
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        /// <param name="creationType">Type of the creation.</param>
        public TValue Resolve<TKey, TValue>(IAssemblyContext context, TKey key, CreationType creationType) where TValue : ILinkable<TKey>
        {
            if (!this.InCache<TValue>(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            GenericLinker<TKey, TValue> linker = this._linkers[typeof(TValue)];

            //Temporary set the creation type for the specified linker, to
            //provide access to the instance creation feature of our GenericLinker class
            linker.CreationType = creationType;

            //Resolve the value.
            TValue value = linker.Resolve(key);

            return value;
        }
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public IEnumerable<TValue> Resolve<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            if (!this.InCache<TValue>(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            return this._linkers[typeof(TValue)];
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public bool InCache<TValue>(IAssemblyContext context)
        {
            return
                this._cachedContexts.ContainsKey(context) && 
                this._linkers.ContainsKey(typeof(TValue));
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public void Cache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            if (!this._linkers.ContainsKey(typeof(TValue)))
            {
                this._linkers.Add(typeof(TValue), new GenericLinker<TKey, TValue>());
            }

            this._cachedContexts.Add(context, true);

            GenericLinker<TKey, TValue> linker = this._linkers[typeof(TValue)];
            foreach (Assembly assembly in context.Assemblies)
            {
                linker.Load(assembly);
            }
        }
        #endregion
    }
}
