using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Script
{
    public class DynamicScriptLoader : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicScriptLoader"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public DynamicScriptLoader(string directory)
        {
            this._watcher = new FileSystemWatcher(directory, "*.cs");

            this._watcher.Changed += (sender, args) =>
            {
            };

            this._watcher.Created += (sender, args) =>
            {
            };

            this._watcher.Deleted += (sender, args) =>
            {
            };

            this._watcher.Renamed += (sender, args) =>
            {
            };
        }
        #endregion

        #region Fields
        private FileSystemWatcher _watcher;
        #endregion
    }
}
