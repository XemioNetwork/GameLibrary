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

        #region Private Methods
        /// <summary>
        /// Gets the default value for the specified type.
        /// </summary>
        private static T GetDefaultValue<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return (T)("Default" as object);
            }

            return default(T);
        }
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="creationType">Type of the creation.</param>
        /// <param name="useDefault">if set to <c>true</c> the default key will be resolved if the specified key does not exist.</param>
        private TValue Resolve<TKey, TValue>(TKey key, CreationType creationType, bool useDefault) where TValue : class, ILinkable<TKey>
        {
            TValue value = this._cache.Resolve<TKey, TValue>(this._context, key, creationType);
            bool isDefaultKey = object.Equals(key, GetDefaultValue<TKey>());

            if (value == default(TValue) && isDefaultKey)
            {
                throw new InvalidOperationException("The specified key does not exist, the ImplementationManager couldn't find any value for the default key instead.");
            }
            if (value == default(TValue) && useDefault)
            {
                return GetNew<TKey, TValue>(GetDefaultValue<TKey>());
            }

            return value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void Register<TKey, TValue>(TValue value) where TValue : ILinkable<TKey>
        {
            this._cache.Register<TKey, TValue>(value);
        }
        /// <summary>
        /// Resolves an instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="useDefault">if set to <c>true</c> the default key will be resolved if the specified key does not exist..</param>
        public TValue GetNew<TKey, TValue>(TKey key, bool useDefault = true) where TValue : class, ILinkable<TKey>
        {
            return this.Resolve<TKey, TValue>(key, CreationType.CreateNew, useDefault);
        }
        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="useDefault">if set to <c>true</c> the default key will be resolved if the specified key does not exist..</param>
        public TValue Get<TKey, TValue>(TKey key, bool useDefault = true) where TValue : class, ILinkable<TKey>
        {
            return this.Resolve<TKey, TValue>(key, CreationType.Singleton, useDefault);
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
            return this._cache.Resolve<TKey, TValue>(this._context);
        }
        #endregion
    }
}
