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
            this._cachedAssemblies = new Dictionary<Assembly, bool>();
            this._linkers = new Dictionary<IAssemblyContext, List<dynamic>>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<Assembly, bool> _cachedAssemblies; 
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
            if (!this.InCache(context))
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
            if (!this.InCache(context))
            {
                this.Cache<TKey, TValue>(context);
            }

            return this._linkers[context]
                .OfType<GenericLinker<TKey, TValue>>()
                .SelectMany(linker => linker);
        }
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <param name="context">The context.</param>
        public bool InCache(IAssemblyContext context)
        {
            return context.Assemblies.All(a => this._cachedAssemblies.ContainsKey(a));
        }
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="context">The context.</param>
        public void Cache<TKey, TValue>(IAssemblyContext context) where TValue : ILinkable<TKey>
        {
            if (!this._linkers.ContainsKey(context))
            {
                this._linkers.Add(context, new List<dynamic>());
            }

            foreach (Assembly assembly in context.Assemblies)
            {
                if (!this._cachedAssemblies.ContainsKey(assembly))
                {
                    var linker = this._linkers[context]
                        .FirstOrDefault(l => l is GenericLinker<TKey, TValue>);

                    if (linker == null)
                    {
                        linker = new GenericLinker<TKey, TValue>();
                        this._linkers[context].Add(linker);
                    }

                    linker.Load(assembly);
                    this._cachedAssemblies.Add(assembly, true);
                }
            }
        }
        #endregion
    }
}
