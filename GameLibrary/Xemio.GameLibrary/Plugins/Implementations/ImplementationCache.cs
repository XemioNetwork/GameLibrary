using System;
using System.Collections.Concurrent;
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
            this._cachedContexts = new Dictionary<IAssemblyContext, HashSet<Type>>();
            this._linkers = new Dictionary<Type, dynamic>();
        }
        #endregion

        #region Fields
        /// <summary>
        /// Holding a reference from a IAssemblyContext to all Types that we're already loaded from that context.
        /// </summary>
        private readonly Dictionary<IAssemblyContext, HashSet<Type>> _cachedContexts;
        /// <summary>
        /// Holding a reference from a already loaded Type to it's linker.
        /// </summary>
        private readonly Dictionary<Type, dynamic> _linkers;
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the linker for the specified key and value combination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        private void InitializeLinker<TKey, TValue>() where TValue : ILinkable<TKey>
        {
            if (!this._linkers.ContainsKey(typeof(TValue)))
            {
                this._linkers.Add(typeof(TValue), new Linker<TKey, TValue>());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            this._cachedContexts.Clear();
            this._linkers.Clear();
        }
        /// <summary>
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void Add<TKey, TValue>(TValue value) where TValue : ILinkable<TKey>
        {
            this.InitializeLinker<TKey, TValue>();

            Linker<TKey, TValue> linker = this._linkers[typeof(TValue)];
            linker.Add(value);
        }
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        /// <param name="creationType">Type of the creation.</param>
        public TValue Get<TKey, TValue>(IAssemblyContext context, TKey key, CreationType creationType) where TValue : ILinkable<TKey>
        {
            this.Cache<TKey, TValue>(context);

            Linker<TKey, TValue> linker = this._linkers[typeof(TValue)];
            linker.CreationType = creationType;
            
            return linker.Resolve(key);
        }
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public IEnumerable<TValue> All<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            this.Cache<TKey, TValue>(context);

            return this._linkers[typeof(TValue)];
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public bool IsCached<TValue>(IAssemblyContext context)
        {
            bool typeCached = this._cachedContexts.ContainsKey(context) && this._cachedContexts[context].Contains(typeof(TValue));
            bool linkerCreated = this._linkers.ContainsKey(typeof(TValue));

            return typeCached && linkerCreated;
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public void Cache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            if (!this.IsCached<TValue>(context))
            {
                this.InitializeLinker<TKey, TValue>();

                if (!this._cachedContexts.ContainsKey(context))
                {
                    this._cachedContexts.Add(context, new HashSet<Type>());
                }

                Linker<TKey, TValue> linker = this._linkers[typeof (TValue)];
                foreach (Assembly assembly in context.Assemblies)
                {
                    linker.Load(assembly);
                }

                this._cachedContexts[context].Add(typeof(TValue));
            }
        }
        #endregion
    }
}
