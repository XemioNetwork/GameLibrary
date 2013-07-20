using System.Collections.Generic;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Plugins.Implementations
{
    public class ImplementationManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationManager"/> class.
        /// </summary>
        public ImplementationManager() : this(ContextFactory.CreateFileAssemblyContext("."))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ImplementationManager(IAssemblyContext context)
        {
            this._context = context;
            this._cache = new ImplementationCache();
        }
        #endregion

        #region Fields
        private readonly IAssemblyContext _context;
        private readonly ImplementationCache _cache;
        #endregion

        #region Methods
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public TValue Get<TKey, TValue>(TKey key) where TValue : class, ILinkable<TKey>
        {
            TValue value = this._cache.Resolve<TKey, TValue>(this._context, key);
            if (value == default(TValue))
            {
                return Get<TKey, TValue>(default(TKey));
            }

            return value;
        }
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public IEnumerable<TValue> All<TKey, TValue>() where TValue : ILinkable<TKey>
        {
            return this._cache.Resolve<TKey, TValue>(this._context);
        }
        #endregion
    }
}
