using System;

namespace Xemio.GameLibrary.Content.Loading
{
    public interface ILoadingHandler
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        int ElementCount { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        float Percentage { get; set; }
        /// <summary>
        /// Called when an asset is loaded. Call Dispose to finalize the loading process for the specified asset.
        /// </summary>
        /// <param name="assetName">The assetName.</param>
        IDisposable OnLoading(string assetName);
    }
}
