using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public interface IContentReader : ILinkable<Type>
    {
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        bool BypassFormat { get; }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object Read(IFormatReader reader);
    }
}
