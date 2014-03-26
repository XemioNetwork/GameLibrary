using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Loading;

namespace Xemio.GameLibrary.Game.Scenes
{
    internal class SceneLoadingReport : ILoadingHandler
    {
        #region Implementation of ILoadingReport
        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public int ElementCount { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }
        /// <summary>
        /// Called when an element is loading.
        /// </summary>
        /// <param name="name">The name.</param>
        public IDisposable OnLoading(string name)
        {
            return ActionDisposable.Empty;
        }
        #endregion
    }
}
