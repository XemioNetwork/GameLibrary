using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Loading
{
    public class ContentLoader : IContentLoader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLoader"/> class.
        /// </summary>
        /// <param assetName="handler">The handler.</param>
        public ContentLoader(ILoadingHandler handler)
        {
            this._handler = handler;
        }
        #endregion

        #region Fields
        private readonly ILoadingHandler _handler;
        #endregion

        #region Implementation of IContentLoader
        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param name="assetName">The assetName.</param>
        /// <param name="action">The action.</param>
        public void Load(string assetName, Action action)
        {
            using (this._handler.OnLoading(assetName))
            {
                action();
            }
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        public void Load<T>(string fileName, Action<T> assignment)
        {
            this.Load(fileName, fileName, assignment);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">The assetName.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        public void Load<T>(string assetName, string fileName, Action<T> assignment)
        {
            using (this._handler.OnLoading(assetName))
            {
                var content = XGL.Components.Require<ContentManager>();
                var value = content.Query<T>(fileName);

                assignment(value);
            }
        }
        /// <summary>
        /// Executes all actions.
        /// </summary>
        void IContentLoader.ExecuteBatchedActions()
        {
        }
        #endregion
    }
}
