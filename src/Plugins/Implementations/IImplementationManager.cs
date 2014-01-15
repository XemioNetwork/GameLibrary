using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Components.Attributes;

namespace Xemio.GameLibrary.Plugins.Implementations
{
    [AbstractComponent]
    public interface IImplementationManager : IComponent
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        IAssemblyContext Context { get; }
        /// <summary>
        /// Merges the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Add(IAssemblyContext context);
        /// <summary>
        /// Registers the specified value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        void Add<TKey, TValue>(TValue value) where TValue : ILinkable<TKey>;
        /// <summary>
        /// Gets the singleton instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        TValue Get<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>;
        /// <summary>
        /// Gets a new instance for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        TValue GetNew<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>;
        /// <summary>
        /// Gets the type for the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        Type GetType<TKey, TValue>(TKey key) where TValue : ILinkable<TKey>;
        /// <summary>
        /// Resolves all instances for the specified type.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        IEnumerable<TValue> All<TKey, TValue>() where TValue : ILinkable<TKey>;
        /// <summary>
        /// Returns a value that determines wether the specified context was already cached or not.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        bool IsCached<TValue>();
        /// <summary>
        /// Caches the specified context.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        void Cache<TKey, TValue>() where TValue : ILinkable<TKey>;
        /// <summary>
        /// Removes the item with the specified identifier from the corresponding linker.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        void Remove<TKey, TValue>(TKey id) where TValue : ILinkable<TKey>;
    }
}
