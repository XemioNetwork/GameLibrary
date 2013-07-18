using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Assets
{
    public class Asset<T> : IAsset
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Asset&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        public Asset(string id, T value)
        {
            this.Id = id;
            this.Value = value;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }
        #endregion

        #region Implementation of IAsset
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region IAsset Member
        /// <summary>
        /// Gets the value.
        /// </summary>
        object IAsset.Value
        {
            get { return this.Value; }
        }
        #endregion
    }
}
