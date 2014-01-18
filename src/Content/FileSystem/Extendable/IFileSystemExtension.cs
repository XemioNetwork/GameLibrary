using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.GameLibrary.Content.FileSystem.Extendable
{
    public interface IFileSystemExtension : ILinkable<string>
    {
        /// <summary>
        /// Gets the file system.
        /// </summary>
        IFileSystem FileSystem { get; }
    }
}
