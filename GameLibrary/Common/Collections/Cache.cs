using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Common.Collections
{
    public class Cache<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="computer">The computer.</param>
        public Cache(IComputationProvider<TKey, TValue> computer)
        {
            this._cache = new Dictionary<TKey, TValue>();
            this.Computer = computer;
        } 
        #endregion

        #region Fields
        private readonly Dictionary<TKey, TValue> _cache;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the computer.
        /// </summary>
        public IComputationProvider<TKey, TValue> Computer { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public TValue Get(TKey key)
        {
            lock (this._cache)
            {
                if (!this._cache.ContainsKey(key))
                {
                    this._cache.Add(key, this.Computer.Compute(key));
                }

                return this._cache[key];
            }
        }
        #endregion
    }
}
