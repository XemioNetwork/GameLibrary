using System.Collections.Generic;
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
            this._linkers = new Dictionary<IAssemblyContext, dynamic>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<IAssemblyContext, dynamic> _linkers;
        #endregion

        #region Methods
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue Resolve<TKey, TValue>(IAssemblyContext context, TKey key) where TValue : ILinkable<TKey>
        {
            if (!this.InCache(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            GenericLinker<TKey, TValue> linker = this._linkers[context];
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
            if (!this.InCache(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            return this._linkers[context] as IEnumerable<TValue>;
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        public bool InCache(IAssemblyContext context)
        {
            return this._linkers.ContainsKey(context);
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public void Cache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            var instance = new GenericLinker<TKey, TValue>();
            instance.Load(context.Assemblies);

            this._linkers.Add(context, instance);
        }
        #endregion
    }
}
