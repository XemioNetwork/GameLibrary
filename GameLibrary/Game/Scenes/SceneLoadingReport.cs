using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Content;

namespace Xemio.GameLibrary.Game.Scenes
{
    internal class SceneLoadingReport : ILoadingReport
    {
        #region Implementation of ILoadingReport
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public int Elements { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }
        /// <summary>
        /// Called when an element is loading.
        /// </summary>
        /// <param name="name">The name.</param>
        public void OnLoading(string name)
        {
        }
        /// <summary>
        /// Called when an element was loaded.
        /// </summary>
        /// <param name="name">The name.</param>
        public void OnLoaded(string name)
        {
        }
        #endregion
    }
}
