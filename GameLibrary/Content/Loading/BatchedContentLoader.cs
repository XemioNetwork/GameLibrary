using System;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Content.Loading
{
    public class BatchedContentLoader : IContentLoader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchedContentLoader" /> class.
        /// </summary>
        /// <param assetName="handler">The handler.</param>
        public BatchedContentLoader(ILoadingHandler handler)
        {
            this._handler = handler;
            this._actions = new List<Action>();
        }
        #endregion

        #region Fields
        private readonly ILoadingHandler _handler;
        private readonly List<Action> _actions; 
        #endregion

        #region Implementation of IContentLoader
        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param assetName="assetName">The assetName.</param>
        /// <param assetName="action">The action.</param>
        public void Load(string assetName, Action action)
        {
            this._actions.Add(() =>
            {
                using (this._handler.OnLoading(assetName))
                {
                    action();
                }
            });
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam assetName="T">The content type.</typeparam>
        /// <param assetName="fileName">Name of the file.</param>
        /// <param assetName="assignment">The assignment.</param>
        public void Load<T>(string fileName, Action<T> assignment)
        {
            this.Load(fileName, fileName, assignment);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam assetName="T">The content type.</typeparam>
        /// <param assetName="assetName">The assetName.</param>
        /// <param assetName="fileName">Name of the file.</param>
        /// <param assetName="assignment">The assignment.</param>
        public void Load<T>(string assetName, string fileName, Action<T> assignment)
        {
            var content = XGL.Components.Require<ContentManager>();
            Action action = () =>
            {
                var value = content.Query<T>(fileName);
                assignment(value);
            };

            this.Load(assetName, action);
        }
        /// <summary>
        /// Loads all queued files.
        /// </summary>
        public void ExecuteBatchedActions()
        {
            this._handler.ElementCount = this._actions.Count;
            for (int i = 0; i < this._actions.Count; i++)
            {
                this._actions[i].Invoke();
                this._handler.Percentage = (i + 1) / (float)this._actions.Count;
            }
        }
        #endregion
    }
}
