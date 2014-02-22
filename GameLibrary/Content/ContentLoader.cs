using System;
using System.Collections.Generic;

namespace Xemio.GameLibrary.Content
{
    public class ContentLoader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLoader" /> class.
        /// </summary>
        /// <param name="report">The report.</param>
        public ContentLoader(ILoadingReport report)
        {
            this._report = report;
            this._actions = new List<Action>();
        }
        #endregion

        #region Fields
        private readonly ILoadingReport _report;
        private readonly List<Action> _actions; 
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        public void Load(string name, Action action)
        {
            this._actions.Add(() =>
            {
                this._report.OnLoading(name);
                action();
                this._report.OnLoaded(name);
            });
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        public void Load<T>(string fileName, Action<T> assignment)
        {
            this.Load(fileName, fileName, assignment);
        }
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        public void Load<T>(string name, string fileName, Action<T> assignment)
        {
            var content = XGL.Components.Require<ContentManager>();
            Action action = () =>
            {
                var value = content.Query<T>(fileName);
                assignment(value);
            };

            this.Load(name, action);
        }
        /// <summary>
        /// Loads all queued files.
        /// </summary>
        public void Execute()
        {
            this._report.Elements = this._actions.Count;
            for (int i = 0; i < this._actions.Count; i++)
            {
                this._actions[i].Invoke();
                this._report.Percentage = (i + 1) / (float)this._actions.Count;
            }
        }
        #endregion
    }
}
