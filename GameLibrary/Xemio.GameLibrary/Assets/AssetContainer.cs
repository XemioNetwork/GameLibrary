using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Assets.Exceptions;

namespace Xemio.GameLibrary.Assets
{
    public class AssetContainer : IEnumerable<IAsset>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetContainer"/> class.
        /// </summary>
        public AssetContainer()
        {
            this._assetMappings = new Dictionary<string, IAsset>();
        }
        #endregion

        #region Fields
        private readonly Dictionary<string, IAsset> _assetMappings;
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified asset.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        public void Add<T>(string id, T value)
        {
            this.Add(new Asset<T>(id, value));
        }
        /// <summary>
        /// Adds the specified asset.
        /// </summary>
        /// <param name="asset">The asset.</param>
        public void Add(IAsset asset)
        {
            this._assetMappings.Add(asset.Id, asset);
        }
        /// <summary>
        /// Determines whether the container contains the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool Contains(string id)
        {
            return this._assetMappings.ContainsKey(id);
        }
        /// <summary>
        /// Gets the asset for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public object Get(string id)
        {
            if (!this._assetMappings.ContainsKey(id))
            {
                throw new AssetNotFoundException(id);
            }

            return this._assetMappings[id].Value;
        }
        /// <summary>
        /// Gets the asset for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public T Get<T>(string id)
        {
            Asset<T> asset = this.Get(id) as Asset<T>;
            if (asset == null)
            {
                return default(T);
            }

            return asset.Value;
        }
        /// <summary>
        /// Removes the asset for the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool Remove(string id)
        {
            return this._assetMappings.Remove(id);
        }
        #endregion

        #region Implementation of IEnumerable
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<IAsset> GetEnumerator()
        {
            return this._assetMappings
                .Select(p => p.Value)
                .GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
