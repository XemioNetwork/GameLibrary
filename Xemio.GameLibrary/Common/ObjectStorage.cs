using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Common
{
    public class ObjectStorage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectStorage"/> class.
        /// </summary>
        public ObjectStorage()
        {
            this._values = new Dictionary<string, object>();
        }
        #endregion

        #region Fields
        private Dictionary<string, object> _values;
        #endregion

        #region Methods
        /// <summary>
        /// Stores the specified value into an internal dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual void Store(string key, object value)
        {
            if (!this._values.ContainsKey(key))
            {
                this._values.Add(key, value);
            }

            this._values[key] = value;
        }
        /// <summary>
        /// Retrieves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public virtual object Retrieve(string key)
        {
            return this._values[key];
        }
        public virtual T Retrieve<T>(string key)
        {
            if (this.Contains(key))
            {
                return (T)this.Retrieve(key);
            }

            return default(T);
        }
        /// <summary>
        /// Determines whether the invokation store contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public virtual bool Contains(string key)
        {
            return this._values.ContainsKey(key);
        }
        /// <summary>
        /// Clears all stored values.
        /// </summary>
        public virtual void Clear()
        {
            this._values.Clear();
        }
        #endregion
    }
}
