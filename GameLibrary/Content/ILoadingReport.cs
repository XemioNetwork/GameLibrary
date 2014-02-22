namespace Xemio.GameLibrary.Content
{
    public interface ILoadingReport
    {
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        int Elements { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        float Percentage { get; set; }
        /// <summary>
        /// Called when an element is loading.
        /// </summary>
        /// <param name="name">The name.</param>
        void OnLoading(string name);
        /// <summary>
        /// Called when an element was loaded.
        /// </summary>
        /// <param name="name">The name.</param>
        void OnLoaded(string name);
    }
}
