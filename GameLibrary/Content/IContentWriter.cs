using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Content.Formats;

namespace Xemio.GameLibrary.Content
{
    public interface IContentWriter : ILinkable<Type>
    {
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        bool BypassFormat { get; }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        void Write(IFormatWriter writer, object value);
    }
}
