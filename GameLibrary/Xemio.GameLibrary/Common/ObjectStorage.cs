using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Conversion;

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
            this._typeParser = new StringParser();
            this._values = new Dictionary<string, object>();

            this.AutoParseStrings = true;
        }
        #endregion

        #region Fields
        private readonly StringParser _typeParser;
        private readonly Dictionary<string, object> _values;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether [auto parse strings].
        /// </summary>
        public bool AutoParseStrings { get; set; }
        #endregion Properties

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
            if (this.Contains(key))
            {
                object value = this._values[key];
                if (value is string && this.AutoParseStrings)
                {
                    value = this._typeParser.Parse(value as string);
                }

                return value;
            }

            return null;
        }
        /// <summary>
        /// Retrieves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public virtual T Retrieve<T>(string key)
        {
            object value = this.Retrieve(key);
            if (value is IConvertible)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            if (value == null)
            {
                return default(T);
            }

            return (T)value;
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
