using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Components;

namespace Xemio.GameLibrary.Content.FileSystem
{
    [AbstractComponent]
    public interface IVirtualFileSystem : IFileSystem
    {
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Load(string fileName);
        /// <summary>
        /// Saves the changes made to the file system.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Persist(string fileName);
    }
}
