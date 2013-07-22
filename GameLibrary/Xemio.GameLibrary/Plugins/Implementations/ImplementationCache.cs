using System.Collections.Generic;
using System.Linq;
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
            this._linkers = new Dictionary<IAssemblyContext, List<dynamic>>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<IAssemblyContext, List<dynamic>> _linkers;
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
            if (!this.InCache<TKey, TValue>(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            GenericLinker<TKey, TValue> linker = this._linkers[context]
                .First(l => l is GenericLinker<TKey, TValue>);

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
            if (!this.InCache<TKey, TValue>(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            return this._linkers[context] as IEnumerable<TValue>;
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public bool InCache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            return this._linkers.ContainsKey(context) &&
                   this._linkers[context].Any(linker => linker is GenericLinker<TKey, TValue>);
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public void Cache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            var linker = new GenericLinker<TKey, TValue>();
            linker.Load(context.Assemblies);

            if (!this._linkers.ContainsKey(context))
            {
                this._linkers.Add(context, new List<dynamic>());
            }

            this._linkers[context].Add(linker);
        }
        #endregion
    }
}
