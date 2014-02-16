using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Xemio.GameLibrary.Content.FileSystem;

namespace Xemio.GameLibrary.Content
{
    internal class ContentTracker : IFileSystemListener
    {
        #region Logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTracker"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public ContentTracker(ContentManager contentManager)
        {
            this._contentManager = contentManager;
        }
        #endregion

        #region Fields
        private readonly ContentManager _contentManager;
        #endregion

        #region Implementation of IFileSystemListener
        /// <summary>
        /// Called when a file system entry changed.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnChanged(string fullPath, string name)
        {
            this._contentManager.Unload(fullPath);
        }
        /// <summary>
        /// Called when a file system entry was created.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnCreated(string fullPath, string name)
        {
            this._contentManager.Unload(fullPath);
        }
        /// <summary>
        /// Called when a file system entry was deleted.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="name">The name.</param>
        public void OnDeleted(string fullPath, string name)
        {
            if (this._contentManager.IsCached(fullPath))
            {
                logger.Warn("File [{0}] was deleted. Keeping old reference to prevent unavailable resource exceptions.");
            }
        }
        #endregion
    }
}
