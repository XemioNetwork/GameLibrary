using System;
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
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void Add<TKey, TValue>(TValue value) where TValue : ILinkable<TKey>
        {
            this._cache.Add<TKey, TValue>(value);
        }
        /// <summary>
        /// Resolves an instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public TValue GetNew<TKey, TValue>(TKey key) where TValue : class, ILinkable<TKey>
        {
            return this._cache.Get<TKey, TValue>(this._context, key, CreationType.CreateNew);
        }
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public TValue Get<TKey, TValue>(TKey key) where TValue : class, ILinkable<TKey>
        {
            return this._cache.Get<TKey, TValue>(this._context, key, CreationType.Singleton);
        }
        /// <summary>
        /// Resolves the type for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        public Type GetType<TKey, TValue>(TKey key) where TValue : class, ILinkable<TKey>
        {
            return this.Get<TKey, TValue>(key).GetType();
        }
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public IEnumerable<TValue> All<TKey, TValue>() where TValue : ILinkable<TKey>
        {
            return this._cache.All<TKey, TValue>(this._context);
        }
        #endregion
    }
}
