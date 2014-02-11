using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xemio.GameLibrary.Content.Formats
{
    public class Metadata
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata()
        {
            this._data = new Dictionary<string, string>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, string> _data; 
        #endregion

        #region Index
        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public string this[string key]
        {
            get
            {
                if (!this._data.ContainsKey(key))
                {
                    return null;
                }
                else
                {
                    return this._data[key];
                }
            }
            set
            {
                if (!this._data.ContainsKey(key))
                {
                    this._data.Add(key, value);
                }
                else
                {
                    this._data[key] = value;
                }
            }
        }
        #endregion
    }
}
