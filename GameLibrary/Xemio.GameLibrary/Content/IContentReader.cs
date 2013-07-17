using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.GameLibrary.Content
{
    public interface IContentReader
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object Read(BinaryReader reader);
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        object Read(string fileName);
    }
}
