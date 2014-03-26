using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xemio.GameLibrary.Content.Loading
{
    public interface IContentLoader
    {
        /// <summary>
        /// Adds the specified action.
        /// </summary>
        /// <param name="assetName">The assetName.</param>
        /// <param name="action">The action.</param>
        void Load(string assetName, Action action);
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        void Load<T>(string fileName, Action<T> assignment);
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">The assetName.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="assignment">The assignment.</param>
        void Load<T>(string assetName, string fileName, Action<T> assignment);
        /// <summary>
        /// Executes all batched actions.
        /// </summary>
        void ExecuteBatchedActions();
    }
}
